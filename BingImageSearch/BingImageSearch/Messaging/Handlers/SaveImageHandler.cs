using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BingImageSearch.Adapters;
using BingImageSearch.Services;
using NotificationsExtensions.ToastContent;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Pickers.Provider;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Notifications;

namespace BingImageSearch.Messaging.Handlers
{
    public class SaveImageHandler : IAsyncHandler<SaveImageMessage>
    {
        private readonly IPickerFactory _pickerFactory;
        private readonly IBackgroundDownloader _backgroundDownloader;
        private readonly IStatusService _statusService;

        public SaveImageHandler(IPickerFactory pickerFactory, IBackgroundDownloader backgroundDownloader, IStatusService statusService)
        {
            _pickerFactory = pickerFactory;
            _backgroundDownloader = backgroundDownloader;
            _statusService = statusService;
        }

        public async Task HandleAsync(SaveImageMessage message)
        {
            // Set up and launch the Open Picker
            var filename = GetFilenameFromUrl(message.Image.MediaUrl);
            var extension = System.IO.Path.GetExtension(filename);
            var picker = _pickerFactory.CreateFileSavePicker();
            picker.SuggestedFileName = filename;
            picker.FileTypeChoices.Add(extension.Trim('.').ToUpper(), new string[] { extension });

            var saveFile = await picker.PickSaveFileAsync();
            if (saveFile != null)
            {
                await _backgroundDownloader.StartDownloadAsync(new Uri(message.Image.MediaUrl), saveFile);

                _statusService.TemporaryMessage = string.Format("Image {0} saved.", saveFile.Name);
            }
        }

        private string GetFilenameFromUrl(string url)
        {
            var uri = new System.Uri(url);
            return uri.Segments[uri.Segments.Length - 1];
        }
    }
}
