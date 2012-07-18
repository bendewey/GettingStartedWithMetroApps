using System.Threading.Tasks;
using Windows.ApplicationModel.Search;

namespace BingImageSearch.Messaging.Handlers
{
    public class ShowSearchPaneHandler : IHandler<ShowSearchPaneMessage>
    {
        public void Handle(ShowSearchPaneMessage message)
        {
            SearchPane.GetForCurrentView().Show();
        }
    }
}