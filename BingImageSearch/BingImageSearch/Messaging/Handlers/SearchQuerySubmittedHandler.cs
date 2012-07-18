using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using BingImageSearch.Model;
using BingImageSearch.Services;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;

namespace BingImageSearch.Messaging.Handlers
{
    public class SearchQuerySubmittedHandler : IAsyncHandler<SearchQuerySubmittedMessage>
    {
        private readonly ApplicationSettings _settings;
        private readonly IImageSearchService _imageSearchService;
        private readonly INavigationService _navigationService;
        private readonly IStatusService _statusService;

        public SearchQuerySubmittedHandler(ApplicationSettings settings, IImageSearchService imageSearchService, INavigationService navigationService, IStatusService statusService)
        {
            _settings = settings;
            _imageSearchService = imageSearchService;
            _navigationService = navigationService;
            _statusService = statusService;
        }

        public async Task HandleAsync(SearchQuerySubmittedMessage message)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                _statusService.SetNetworkUnavailable();
                return;
            }

            _statusService.Message = "Loading Images for " + message.Query;
            _statusService.IsLoading = true;

            try
            {
                // Remove any existing searches for this query
                var searches = _settings.Searches;
                var existing = searches.FirstOrDefault(s => s.Query.Equals(message.Query, StringComparison.CurrentCultureIgnoreCase));
                if (existing != null)
                {
                    searches.Remove(existing);
                }

                // Search Bing
                var images = await _imageSearchService.Search(message.Query, _settings.Rating, _settings.ImageResultSize);
                if (!images.Any())
                {
                    _statusService.SetBingUnavailable();
                    return;
                }

                // Store results in app settings
                var instance = new SearchInstance()
                                   {
                                       Images = images,
                                       SearchedOn = DateTime.Today,
                                       Query = message.Query
                                   };
                searches.Insert(0, instance);
                _settings.Searches = searches;
                _settings.SelectedInstance = instance;
                await _settings.SaveAsync();

                // Navigate
                _navigationService.Navigate(typeof(SearchResultsPage));
            }
            catch (InvalidOperationException ex)
            {
                var baseEx = ex.GetBaseException();
                if (baseEx is WebException)
                {
                    _statusService.SetBingUnavailable();
                    return;
                }
                throw;
            }
            finally
            {
                _statusService.IsLoading = false;
            }
        }
    }
}
