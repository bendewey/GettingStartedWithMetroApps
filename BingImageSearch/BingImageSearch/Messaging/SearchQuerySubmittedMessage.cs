namespace BingImageSearch.Messaging
{
    public class SearchQuerySubmittedMessage : IMessage
    {
        public string Query { get; private set; }

        public SearchQuerySubmittedMessage(string query)
        {
            Query = query;
        }
    }
}
