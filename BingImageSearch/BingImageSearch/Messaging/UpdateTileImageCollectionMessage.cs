using BingImageSearch.Model;

namespace BingImageSearch.Messaging
{
    public class UpdateTileImageCollectionMessage : IMessage
    {
        public SearchInstance Instance { get; private set; }

        public UpdateTileImageCollectionMessage(SearchInstance instance)
        {
            Instance = instance;
        }
    }
}
