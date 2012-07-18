using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Input;
using BingImageSearch.Adapters;
using BingImageSearch.Common;
using BingImageSearch.Common;
using BingImageSearch.Messaging;
using BingImageSearch.Services;
using Windows.UI.Xaml.Navigation;

namespace BingImageSearch.Model
{
    public class SearchResultsPageViewModes
    {
        public static string SplitView = "SplitView";
        public static string ThumbnailView = "ThumbnailView";
        public static string ListView = "ListView";
    }

    public class SearchResultsPageViewModel : BindableBase
    {
        private readonly ApplicationSettings _settings;
        private readonly INavigationService _navigationService;
        private readonly IImageSearchService _imageSearchService;
        private readonly IHub _hub;
        private readonly IAccelerometer _accelerometer;
        private readonly IStatusService _statusService;

        public SearchResultsPageViewModel(ApplicationSettings settings, INavigationService navigationService, IImageSearchService imageSearchService, IHub hub, IAccelerometer accelerometer, IStatusService statusService, IShareDataRequestedPump shareMessagePump)
        {
            _settings = settings;
            _navigationService = navigationService;
            _imageSearchService = imageSearchService;
            _hub = hub;
            _accelerometer = accelerometer;
            _statusService = statusService;

            HomeCommand = _navigationService.GoBackCommand;
            ViewDetailsCommand = new DelegateCommand(ViewDetails, () => SelectedImage != null);
            LoadMoreCommand = new AsyncDelegateCommand(LoadMore);
            ThumbnailViewCommand = new DelegateCommand(ThumbnailView);
            ListViewCommand = new DelegateCommand(ListView);
            SplitViewCommand = new DelegateCommand(SplitView);
            SettingsCommand = new DelegateCommand(Settings);

            AddImages(_settings.SelectedInstance.Images);
            shareMessagePump.DataToShare = _settings.SelectedInstance.QueryLink;
            _statusService.Title = _settings.SelectedInstance.Query;
            _accelerometer.Shaken += accelerometer_Shaken;
            _navigationService.Navigating += NavigatingFrom;

            UpdateCurrentView(CurrentView);
            _hub.Send(new UpdateTileImageCollectionMessage(_settings.SelectedInstance));
        }

        public ICommand HomeCommand { get; set; }
        public ICommand ViewDetailsCommand { get; set; }
        public ICommand LoadMoreCommand { get; set; }
        public ICommand SplitViewCommand { get; set; }
        public ICommand ThumbnailViewCommand { get; set; }
        public ICommand ListViewCommand { get; set; }
        public ICommand SettingsCommand { get; set; }

        private ObservableCollection<ImageResult> _images;
        public ObservableCollection<ImageResult> Images
        {
            get { return _images; }
        }

        public SearchInstance SelectedInstance
        {
            get { return _settings.SelectedInstance; }
        }

        public ImageResult SelectedImage
        {
            get { return _settings.SelectedImage;  }
            set
            {
                _settings.SelectedImage = value;
                OnPropertyChanged();
                ((DelegateCommand)ViewDetailsCommand).RaiseCanExecuteChanged();
            }
        }

        private void NavigatingFrom(object sender, NavigatingCancelEventArgs e)
        {
            _accelerometer.Shaken -= accelerometer_Shaken;
            _navigationService.Navigating -= NavigatingFrom;
        }

        private async void accelerometer_Shaken(object sender, object e)
        {
            await LoadMore();
        }

        private void AddImages(IEnumerable<ImageResult> imagesToAdd)
        {
            if (_images == null)
            {
                _images = new ObservableCollection<ImageResult>();
            }

            foreach (var image in imagesToAdd.Where(img => !_images.Contains(img)))
            {
                _images.Add(image);
            }
        }

        public async Task LoadMore()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                _statusService.SetNetworkUnavailable();
                return;
            }

            _statusService.IsLoading = true;
            _statusService.Message = "Loading more images for " + _settings.SelectedInstance.Query;
            await _imageSearchService.LoadMore(_settings.SelectedInstance, _settings.Rating, _settings.ImageResultSize);
            _statusService.IsLoading = false;
            AddImages(_settings.SelectedInstance.Images);
            await _settings.SaveAsync();
        }

        public string CurrentView
        {
            get { return _settings.SearchResultsCurrentView; }
            set
            {
                _settings.SearchResultsCurrentView = value;
                UpdateCurrentView(value);
            }
        }

        private void UpdateCurrentView(string view)
        {
            IsThumbnailViewVisible = view == SearchResultsPageViewModes.ThumbnailView ? true : false;
            IsListViewVisible = view == SearchResultsPageViewModes.ListView ? true : false;
            IsSplitViewVisible = view == SearchResultsPageViewModes.SplitView ? true : false;
        }

        private bool _isSplitViewVisible;
        public bool IsSplitViewVisible
        {
            get { return _isSplitViewVisible; }
            set
            {
                SetProperty(ref _isSplitViewVisible, value);
            }
        }

        private bool _isThumbnailViewVisible;
        public bool IsThumbnailViewVisible
        {
            get { return _isThumbnailViewVisible; }
            set
            {
                SetProperty(ref _isThumbnailViewVisible, value);
            }
        }

        private bool _isListViewVisible;
        public bool IsListViewVisible
        {
            get { return _isListViewVisible; }
            set
            {
                SetProperty(ref _isListViewVisible, value);
            }
        }

        public void SplitView()
        {
            CurrentView = SearchResultsPageViewModes.SplitView;
        }

        public void ThumbnailView()
        {
            CurrentView = SearchResultsPageViewModes.ThumbnailView;
        }

        public void ListView()
        {
            CurrentView = SearchResultsPageViewModes.ListView;
        }

        public void ViewDetails()
        {
            _navigationService.Navigate(typeof(DetailsPage));
        }

        public void Settings()
        {
            _hub.Send(new ShowSettingsMessage());
        }

        public void DecreaseView()
        {
            if (CurrentView == SearchResultsPageViewModes.SplitView)
            {
                CurrentView = SearchResultsPageViewModes.ListView;
            }
            else if (CurrentView == SearchResultsPageViewModes.ListView)
            {
                CurrentView = SearchResultsPageViewModes.ThumbnailView;
            }
        }

        public void IncreaseView()
        {
            if (CurrentView == SearchResultsPageViewModes.ListView)
            {
                CurrentView = SearchResultsPageViewModes.SplitView;
            }
            else if (CurrentView == SearchResultsPageViewModes.ThumbnailView)
            {
                CurrentView = SearchResultsPageViewModes.ListView;
            }
        }
    }
}
