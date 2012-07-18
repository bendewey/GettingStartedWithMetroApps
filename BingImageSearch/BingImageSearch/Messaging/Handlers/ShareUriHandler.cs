namespace BingImageSearch.Messaging.Handlers
{
    public class ShareUriHandler : IHandler<ShareUriMessage>
    {
        public void Handle(ShareUriMessage message)
        {
            var request = message.DataRequestedEventArgs.Request;

            request.Data.Properties.Title = "Bing Image Search Link";
            request.Data.SetUri(message.Link);
        }
    }
}
