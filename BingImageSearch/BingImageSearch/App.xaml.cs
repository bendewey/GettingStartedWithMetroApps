using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BingImageSearch;
using BingImageSearch.Common;
using BingImageSearch.Messaging;
using BingImageSearch.Services;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace BingImageSearch
{
    partial class App
    {
        private Shell shell;
        private static Type[] _messagePumpTypes = new[] { typeof(SearchPaneMessagePump), typeof(IShareDataRequestedPump) };
        private Lazy<IMessagePump[]> _messagePumps = new Lazy<IMessagePump[]>(_messagePumpTypes.Select(t => ViewModelLocator.Container.Resolve(t) as IMessagePump).ToArray);

        public IEnumerable<IMessagePump> MessagePumps
        {
            get { return _messagePumps.Value; }
        }

        public static ViewModelLocator ViewModelLocator
        {
            get { return (ViewModelLocator)Current.Resources["ViewModelLocator"]; }
        }

        public static CoreDispatcher Dispatcher { get; set; }

        public App()
        {
            InitializeComponent();

            UnhandledException += OnUnhandledException;
        }

        void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            OnUnhandledException(e.Exception);
        }

        public async void OnUnhandledException(Exception exception)
        {
            var dispatcher = App.Dispatcher;
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, (DispatchedHandler)(async () =>
            {
#if DEBUG
                var baseEx = exception.GetBaseException();
                var message = exception.Message;
                if (baseEx != exception)
                {
                    message += "\r\n\r\n" + baseEx.Message;
                }
                await ViewModelLocator.DialogService.ShowMessageAsync(message);
#else
                await ViewModelLocator.DialogService.ShowResourceMessageAsync("Exception_UnhandledException");
                Exit();
#endif
            }));
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            try
            {
                await EnsureShell(args.PreviousExecutionState);
                ViewModelLocator.NavigationService.Navigate(typeof(SearchHistoryPage));
            }
            catch (Exception ex)
            {
                // Due to an issues I've noted online: http://social.msdn.microsoft.com/Forums/en/winappswithcsharp/thread/bea154b0-08b0-4fdc-be31-058d9f5d1c4e
                // I am limiting the use of 'async void'  In a few rare occasions I use it
                // and manually route the exceptions to the UnhandledExceptionHandler
                ((App)App.Current).OnUnhandledException(ex);
            }
        }

        protected override async void OnSearchActivated(SearchActivatedEventArgs args)
        {
            try
            {
                await EnsureShell(args.PreviousExecutionState);
                await ViewModelLocator.Hub.Send(new SearchQuerySubmittedMessage(args.QueryText));
            }
            catch (Exception ex)
            {
                // Due to an issues I've noted online: http://social.msdn.microsoft.com/Forums/en/winappswithcsharp/thread/bea154b0-08b0-4fdc-be31-058d9f5d1c4e
                // I am limiting the use of 'async void'  In a few rare occasions I use it
                // and manually route the exceptions to the UnhandledExceptionHandler
                ((App)App.Current).OnUnhandledException(ex);
            }
        }

        protected override void OnFileOpenPickerActivated(FileOpenPickerActivatedEventArgs args)
        {
            ViewModelLocator.FileOpenPickerUiManager.Initialize(args.FileOpenPickerUI);
            Window.Current.Content = new FileOpenPickerPage();
            Window.Current.Activate();
        }

        private async Task EnsureShell(ApplicationExecutionState previousState)
        {
            if (previousState == ApplicationExecutionState.Running)
            {
                Window.Current.Activate();
                ViewModelLocator.NavigationService.InitializeFrame(shell.Frame);
                return;
            }

            if (previousState == ApplicationExecutionState.Terminated || previousState == ApplicationExecutionState.ClosedByUser)
            {
                var settings = ViewModelLocator.Container.Resolve<ApplicationSettings>();
                await settings.RestoreAsync();
            }

            shell = new Shell();
            ViewModelLocator.NavigationService.InitializeFrame(shell.Frame);
            Window.Current.Content = shell;
            Window.Current.Activate();
            Dispatcher = Window.Current.CoreWindow.Dispatcher;

            MessagePumps.StartAll();

            ViewModelLocator.ShellViewModel.IsLoading = false;
        }
    }
}
