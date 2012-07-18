using System;
using BingImageSearch.Adapters;
using Windows.ApplicationModel.DataTransfer;

namespace BingImageSearch.Messaging
{
    public class ShareUriMessage : ShareDataMessage
    {
        public Uri Link { get; private set; }

        public ShareUriMessage(Uri link, DataTransferManager sender, SettableDataRequestedEventArgs args)
            : base(sender, args)
        {
            Link = link;
        }
    }
}
