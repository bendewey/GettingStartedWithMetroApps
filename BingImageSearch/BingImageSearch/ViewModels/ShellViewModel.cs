using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BingImageSearch.Common;
using BingImageSearch.Common;
using BingImageSearch.Messaging;
using BingImageSearch.Services;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Input;

namespace BingImageSearch.Model
{
    public class ShellViewModel : BindableBase, IStatusService
    {
        private readonly AppName _appName;
        private readonly ApplicationSettings _settings;
        private readonly INavigationService _navigationService;
        private readonly IHub _hub;
        private readonly ResourceLoader _resourceLoader = new ResourceLoader();

        public ShellViewModel(AppName appName, ApplicationSettings settings, INavigationService navigationService, IHub hub)
        {
            _appName = appName;
            _settings = settings;
            _navigationService = navigationService;
            _hub = hub;

            Message = "Initializing...";
            IsLoading = true;

            BackCommand = _navigationService.GoBackCommand;
        }

        public ICommand BackCommand { get; set; }

        public string Title
        {
            get { return _settings.CurrentPageTitle ?? _appName; }
            set
            {
                _settings.CurrentPageTitle = value;
                base.OnPropertyChanged();
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                SetProperty(ref _isLoading, value);
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if (value != null)
                {
                    CommandText = null;
                    ErrorMessage = null;
                }
                SetProperty(ref _message, value);
            }
        }

        private string _temporaryMessage;
        public string TemporaryMessage
        {
            get { return _temporaryMessage; }
            set
            {
                if (value != null)
                {
                    // always reset the message first.
                    TemporaryMessage = null;
                }
                SetProperty(ref _temporaryMessage, value);
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                SetProperty(ref _errorMessage, value);
            }
        }

        private string _commandText;
        public string CommandText
        {
            get { return _commandText; }
            set
            {
                if (value != null)
                {
                    Message = null;
                }
                SetProperty(ref _commandText, value);
            }
        }

        private ICommand _statusCommand;
        public ICommand StatusCommand
        {
            get { return _statusCommand; }
            set
            {
                SetProperty(ref _statusCommand, value);
            }
        }

        public void SetNetworkUnavailable()
        {
            CommandText = "Settings";
            StatusCommand = new DelegateCommand(ShowSettings);
            ErrorMessage = _resourceLoader.GetString("Exception_NetworkNotAvailable");
        }

        public void SetBingUnavailable()
        {
            CommandText = "Settings";
            StatusCommand = new DelegateCommand(ShowSettings);
            ErrorMessage = _resourceLoader.GetString("Exception_BingUnavailable");
        }

        private void ShowSettings()
        {
            _hub.Send(new ShowSettingsMessage());
        }
    }
}
