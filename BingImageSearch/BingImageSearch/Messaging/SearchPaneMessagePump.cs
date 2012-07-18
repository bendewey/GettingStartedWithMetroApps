
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Search;

namespace BingImageSearch.Messaging
{
    public class SearchPaneMessagePump : IMessagePump
    {
        private readonly IHub _messageHub;
        private SearchPane _searchPane;

        public SearchPaneMessagePump(IHub messageHub)
        {
            _messageHub = messageHub;
        }

        public void Start()
        {
            _searchPane = SearchPane.GetForCurrentView();
            _searchPane.QuerySubmitted += this.OnQuerySubmitted;
        }

        public void Stop()
        {
            if (_searchPane != null)
            {
                _searchPane.QuerySubmitted -= this.OnQuerySubmitted;   
            }
        }

        private async void OnQuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs args)
        {
            try
            {
                await _messageHub.Send(new SearchQuerySubmittedMessage(args.QueryText));
            }
            catch (Exception ex)
            {
                // Due to an issues I've noted online: http://social.msdn.microsoft.com/Forums/en/winappswithcsharp/thread/bea154b0-08b0-4fdc-be31-058d9f5d1c4e
                // I am limiting the use of 'async void'  In a few rare occasions I use it
                // and manually route the exceptions to the UnhandledExceptionHandler
                ((App)App.Current).OnUnhandledException(ex);
            }
        }
    }
}