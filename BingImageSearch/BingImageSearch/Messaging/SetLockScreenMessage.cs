using BingImageSearch.Model;

namespace BingImageSearch.Messaging
{
    public class SetLockScreenMessage : IMessage
    {
        public ImageResult Image { get; set; }

        public SetLockScreenMessage(ImageResult image)
        {
            Image = image;
        }
    }
}