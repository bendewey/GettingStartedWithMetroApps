using System;
using BingImageSearch.Services;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;

namespace BingImageSearch.Messaging.Handlers
{
    public class ShareImageResultsHandler : IHandler<ShareImageResultsMessage>
    {
        private readonly ApplicationSettings _settings;

        public ShareImageResultsHandler(ApplicationSettings settings)
        {
            _settings = settings;
        }

        public void Handle(ShareImageResultsMessage message)
        {
            var image = message.Image;
            if (image.MediaUrl != null)
            {
                var request = message.DataRequestedEventArgs.Request;
                request.Data.Properties.Title = "Bing Search Image";
                request.Data.Properties.Description = string.Format("Sharing {0} originally from {1}", image.Title, image.MediaUrl);
                request.Data.SetDataProvider(StandardDataFormats.Bitmap, async dpr =>
                {
                    var deferral = dpr.GetDeferral();

                    var shareFile = await _settings.GetShareFileAsync(image.MediaUrl);
                    var stream = await shareFile.OpenAsync(FileAccessMode.Read);
                    dpr.SetData(RandomAccessStreamReference.CreateFromStream(stream));

                    deferral.Complete();
                });
            }
        }
    }
}
