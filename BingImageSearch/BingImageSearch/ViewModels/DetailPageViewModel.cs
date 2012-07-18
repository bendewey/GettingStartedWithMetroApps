using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BingImageSearch.Adapters;
using BingImageSearch.Common;
using BingImageSearch.Messaging;
using BingImageSearch.Services;
using NotificationsExtensions.ToastContent;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace BingImageSearch.Model
{
    public class DetailsPageViewModel
    {
        private readonly ApplicationSettings _settings;
        private readonly INavigationService _navigationService;
        private readonly IHub _messageHub;
        private readonly IShareDataRequestedPump _shareMessagePump;
        private readonly IStatusService _statusService;

        public DetailsPageViewModel(ApplicationSettings settings, INavigationService navigationService, IHub messageHub, IShareDataRequestedPump shareMessagePump, IStatusService statusService)
        {
            _settings = settings;
            _navigationService = navigationService;
            _messageHub = messageHub;
            _shareMessagePump = shareMessagePump;
            _statusService = statusService;

            statusService.Title = _settings.SelectedImage.Title;
            _shareMessagePump.DataToShare = _settings.SelectedImage;

            BackCommand = _navigationService.GoBackCommand;
            SaveCommand = new AsyncDelegateCommand(Save);
            SetLockScreenCommand = new AsyncDelegateCommand(SetLockScreen);
            SetTileCommand = new DelegateCommand(SetTile);
            ShareCommand = new DelegateCommand(Share);
            SettingsCommand = new DelegateCommand(Settings);
        }

        public ICommand BackCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SetLockScreenCommand { get; set; }
        public ICommand SetTileCommand { get; set; }
        public ICommand ShareCommand { get; set; }
        public ICommand SettingsCommand { get; set; }

        public object SelectedImage
        {
            get { return _settings.SelectedImage; }
        }

        public async Task Save()
        {
            await _messageHub.Send(new SaveImageMessage(_settings.SelectedImage));   
        }

        public async Task SetLockScreen()
        {
            await _messageHub.Send(new SetLockScreenMessage(_settings.SelectedImage));
        }

        public void SetTile()
        {
            _messageHub.Send(new UpdateTileMessage(_settings.SelectedImage));
        }

        public void Share()
        {
            _messageHub.Send(new ShowShareUiMessage());
        }

        public void Settings()
        {
            _messageHub.Send(new ShowSettingsMessage());
        }
    }
}
