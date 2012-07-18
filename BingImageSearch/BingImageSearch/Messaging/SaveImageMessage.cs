using BingImageSearch.Model;

namespace BingImageSearch.Messaging
{
    public class SaveImageMessage : IMessage
    {
        public ImageResult Image { get; private set; }

        public SaveImageMessage(ImageResult image)
        {
            Image = image;
        }
    }
}