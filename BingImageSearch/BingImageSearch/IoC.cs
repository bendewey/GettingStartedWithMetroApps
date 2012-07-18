using System;
using MetroIoc;
using BingImageSearch.Adapters;
using BingImageSearch.Common;
using BingImageSearch.Messaging;
using BingImageSearch.Messaging.Handlers;
using BingImageSearch.Model;
using BingImageSearch.Services;
using Windows.Devices.Sensors;

namespace BingImageSearch
{
    class IoC
    {
        public static IContainer BuildContainer()
        {
            var container = new MetroContainer();
            container.RegisterInstance(container);
            container.RegisterInstance<IContainer>(container);

            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                container.Register<IBackgroundDownloader, NullBackgroundDownloader>();
                container.RegisterInstance<ApplicationSettings>(new NullApplicationSettings());
                container.Register<IImageSearchService, NullImageSearchService>();
            }
            else
            {
                container.Register<IBackgroundDownloader, BackgroundDownloaderAdapter>();
                container.RegisterInstance<ApplicationSettings>(new ApplicationSettings(container.Resolve<IBackgroundDownloader>()));
                container.RegisterInstance<BingApi>("<Account Key>");
                container.Register<IImageSearchService, BingImageSearchService>();
            }

            container.RegisterInstance<AppName>("PhotoZoom Navigation");
            container.RegisterInstance<IDataTransferManager>(new DataTransferManagerAdapter());
            container.RegisterInstance<ILockScreen>(new LockScreenAdapter());
            container.Register<IAccelerometer, AccelerometerAdapter>(lifecycle: new SingletonLifecycle());
            container.Register<INavigationService, NavigationService>(lifecycle: new SingletonLifecycle());
            container.Register<IFileOpenPickerUiManager, FileOpenPickerUiManager>(lifecycle: new SingletonLifecycle());
            container.Register<IHub, MessageHub>(lifecycle: new SingletonLifecycle());
            container.Register<IShareDataRequestedPump, ShareDataRequestedPump>(lifecycle: new SingletonLifecycle());
            container.Register<IDialogService, DialogService>();
            container.Register<ITileUpdateManager, TileUpdateManagerAdapter>();
            container.Register<IToastNotificationManager, ToastNotificationManagerAdapter>();
            container.Register<IPickerFactory, PickerFactory>();

            RegisterHandlers(container);

            container.Register<ShellViewModel>(lifecycle: new SingletonLifecycle());
            // I would prefer not to immediately resolve a singleton, but the MetroContainer doesn't support resolving from Method or provider alternatives.
            container.RegisterInstance<IStatusService>(container.Resolve<ShellViewModel>());

            return container;
        }

        private static void RegisterHandlers(IContainer container)
        {
            // todo: Add Assembly Scanning here to auto-register
            container.Register<IHandler<ShowSearchPaneMessage>, ShowSearchPaneHandler>();
            container.Register<IHandler<ShareImageResultsMessage>, ShareImageResultsHandler>();
            container.Register<IHandler<ShareUriMessage>, ShareUriHandler>();
            
            container.Register<IHandler<ShowShareUiMessage>, ShowShareUiHandler>();
            container.Register<IHandler<UpdateTileMessage>, UpdateTileHandler>();
            container.Register<IHandler<UpdateTileImageCollectionMessage>, UpdateTileImageCollectionHandler>();
            container.Register<IHandler<ShowSettingsMessage>, ShowSettingsHandler>();
            container.Register<IAsyncHandler<SearchQuerySubmittedMessage>, SearchQuerySubmittedHandler>();
            container.Register<IAsyncHandler<SetLockScreenMessage>, SetLockScreenHandler>();
            container.Register<IAsyncHandler<SaveImageMessage>, SaveImageHandler>();
        }
    }
}
