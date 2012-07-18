using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;

namespace BingImageSearch.Adapters
{
    public interface IDataTransferManager
    {
        event TypedEventHandler<DataTransferManager, SettableDataRequestedEventArgs> DataRequested;
        void ShowShareUI();
    }
}