using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingImageSearch.Messaging;
using BingImageSearch.Model;

namespace BingImageSearch.Tests.Messaging
{
    public static class MessagingTestExtensions
    {
        public static bool SentWith(this SaveImageMessage message, ImageResult image)
        {
            return message != null && message.Image == image;
        }

        public static bool SentWith(this SetLockScreenMessage message, ImageResult image)
        {
            return message != null && message.Image == image;
        }

        public static bool SentWith(this UpdateTileMessage message, ImageResult image)
        {
            return message != null && message.Image == image;
        }
    }
}