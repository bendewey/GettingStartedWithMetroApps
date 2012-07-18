using System;
using BingImageSearch.Model;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using BingImageSearch.Services;
using BingImageSearch.Adapters;

namespace BingImageSearch.Messaging
{
    public interface IShareDataRequestedPump : IMessagePump
    {
        object DataToShare { set; }
    }

    public class ShareDataRequestedPump : IShareDataRequestedPump
    {
        private readonly IDataTransferManager _dataTransferManager;
        private readonly IHub _hub;

        public ShareDataRequestedPump(IDataTransferManager dataTransferManager, IHub hub)
        {
            _dataTransferManager = dataTransferManager;
            _hub = hub;
        }
        
        public void Start()
        {
            _dataTransferManager.DataRequested += OnDataRequested;
        }

        public void Stop()
        {
            _dataTransferManager.DataRequested -= OnDataRequested;
        }

        public object DataToShare { get; set; }

        void OnDataRequested(DataTransferManager sender, SettableDataRequestedEventArgs args)
        {
            if (DataToShare == null) return;

            if (DataToShare is Uri)
            {
                var message = new ShareUriMessage((Uri)DataToShare, sender, args);
                _hub.Send(message);
                return;
            }

            if (DataToShare is ImageResult)
            {
                var message = new ShareImageResultsMessage((ImageResult)DataToShare, sender, args);
                _hub.Send(message);
                return;
            }
        }
    }
}