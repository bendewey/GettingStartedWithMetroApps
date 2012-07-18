using System;
using BingImageSearch.Adapters;
using BingImageSearch.Model;
using Windows.ApplicationModel.DataTransfer;

namespace BingImageSearch.Messaging
{
    public class ShareImageResultsMessage : ShareDataMessage
    {
        public ImageResult Image { get; private set; }

        public ShareImageResultsMessage(ImageResult image, DataTransferManager sender, SettableDataRequestedEventArgs args)
            : base(sender, args)
        {
            Image = image;
        }
    }
}
