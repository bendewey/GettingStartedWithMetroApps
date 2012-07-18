using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingImageSearch.Adapters;
using Windows.ApplicationModel.DataTransfer;

namespace BingImageSearch.Messaging
{
    public abstract class ShareDataMessage : IMessage
    {
        public DataTransferManager Sender { get; private set; }
        public SettableDataRequestedEventArgs DataRequestedEventArgs { get; private set; }

        public ShareDataMessage(DataTransferManager sender, SettableDataRequestedEventArgs args)
        {
            Sender = sender;
            DataRequestedEventArgs = args;
        }
    }
}
