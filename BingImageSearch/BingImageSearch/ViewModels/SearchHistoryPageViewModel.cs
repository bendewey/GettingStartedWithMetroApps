using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BingImageSearch.Common;
using BingImageSearch.Common;
using BingImageSearch.Messaging;
using BingImageSearch.Services;
using Windows.UI.Xaml.Input;

namespace BingImageSearch.Model
{
    public class SearchHistoryPageViewModel : BindableBase
    {
        private readonly ApplicationSettings _settings;
        private readonly INavigationService _navigationService;
        private readonly IHub _messageHub;
        private readonly IStatusService _statusService;

        public SearchHistoryPageViewModel(ApplicationSettings settings, INavigationService navigationService, IHub messageHub, IStatusService statusService)
        {
            _settings = settings;
            _navigationService = navigationService;
            _messageHub = messageHub;
            _statusService = statusService;

            _statusService.Title = "Searches";
            _settings.SelectedInstance = null;
            SearchCommand = new DelegateCommand(Search);
            SettingsCommand = new DelegateCommand(Settings);
            SearchQueryCommand = new AsyncDelegateCommand<string>(SearchQuery);
        }

        public ICommand SearchCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand SearchQueryCommand { get; set; }
       
        public void Search()
        {
            _messageHub.Send(new ShowSearchPaneMessage());
        }

        public void Settings()
        {
            _messageHub.Send(new ShowSettingsMessage());
        }

        private async Task SearchQuery(string query)
        {
            await _messageHub.Send(new SearchQuerySubmittedMessage(query));
        }

        public List<SearchInstance> Searches
        {
            get { return _settings.Searches; }
        }

        public List<SearchInstance> Suggestions
        {
            get { return _settings.Searches; }
        }
        

        public SearchInstance SelectedItem
        {
            get { return _settings.SelectedInstance; }
            set
            {
                if (value != null)
                {
                    _settings.SelectedInstance = value;
                    _navigationService.Navigate(typeof(SearchResultsPage));
                }
            }
        }
    }
}