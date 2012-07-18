using BingImageSearch.Model;

namespace BingImageSearch.Messaging
{
    public class UpdateTileMessage : IMessage
    {
        public ImageResult Image { get; private set; }

        public UpdateTileMessage(ImageResult image)
        {
            Image = image;
        }
    }
}