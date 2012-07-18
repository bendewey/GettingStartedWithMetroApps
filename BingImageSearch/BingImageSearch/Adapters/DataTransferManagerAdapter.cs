using BingImageSearch.Common;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using System;

namespace BingImageSearch.Adapters
{
    internal class DataTransferManagerAdapter : IDataTransferManager
    {
        private readonly Lazy<DataTransferManager> _currentView;

        private TypedEventHandler<DataTransferManager, SettableDataRequestedEventArgs> _dataRequested;
        public event TypedEventHandler<DataTransferManager, SettableDataRequestedEventArgs> DataRequested
        {
            add 
            {
                if (_dataRequested != null && _dataRequested.GetInvocationList().Any())
                {
                    throw new Exception("WinRT does not allow more then one handler for the DataTransferManager.DataRequested event.");
                }
                _dataRequested += value;
                _currentView.Value.DataRequested += DataTransferManager_DataRequested;
            }
            remove
            {
                _dataRequested -= value;
                _currentView.Value.DataRequested -= DataTransferManager_DataRequested;
            }
        }

        void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        {
            if (_dataRequested != null)
            {
                _dataRequested((DataTransferManager)sender, new SettableDataRequestedEventArgs(e));
            }
        }

        public DataTransferManagerAdapter()
        {
            _currentView = new Lazy<DataTransferManager>(() => DataTransferManager.GetForCurrentView());
        }

        public void ShowShareUI()
        {
            DataTransferManager.ShowShareUI();
        }
    }

    public class SettableDataRequestedEventArgs
    {
        public SettableDataRequestedEventArgs(DataRequestedEventArgs dataRequestedArgs)
        {
            Request = new SettableDataRequest(dataRequestedArgs.Request);
        }

        public SettableDataRequestedEventArgs()
        {
        }

        public SettableDataRequest Request { get; set; }
    }

    public class SettableDataRequest
    {
        private Lazy<IDeferral> _deferral;
        public SettableDataRequest(DataRequest dataRequest)
        {
            Data = dataRequest.Data;
            _deferral = new Lazy<IDeferral>(() => new DataRequestDeferralAdapter(dataRequest.GetDeferral()));
        }

        public SettableDataRequest()
        {
        }

        public DataPackage Data { get; set; }
        public IDeferral Deferral 
        { 
            get { return _deferral.Value; }
            set { _deferral = new Lazy<IDeferral>(() => value); }
        }

        public IDeferral GetDeferral()
        {
            return Deferral;
        }
    }

    public interface IDeferral
    {
        void Complete();
    }

    public class DataRequestDeferralAdapter : IDeferral
    {
        private DataRequestDeferral _dataRequestDeferral;
        public DataRequestDeferralAdapter(DataRequestDeferral dataRequestDeferral)
        {
            _dataRequestDeferral = dataRequestDeferral;
        }

        public void Complete()
        {
            _dataRequestDeferral.Complete();
        }
    }
}
