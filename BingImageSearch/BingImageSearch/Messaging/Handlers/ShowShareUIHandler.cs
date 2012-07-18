using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace BingImageSearch.Messaging.Handlers
{
    public class ShowShareUiHandler : IHandler<ShowShareUiMessage>
    {
        public void Handle(ShowShareUiMessage message)
        {
            DataTransferManager.ShowShareUI();
        }
    }
}