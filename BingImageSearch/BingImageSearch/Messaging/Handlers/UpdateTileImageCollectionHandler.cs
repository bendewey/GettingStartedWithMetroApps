using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BingImageSearch.Adapters;
using BingImageSearch.Model;
using NotificationsExtensions;
using NotificationsExtensions.TileContent;

namespace BingImageSearch.Messaging.Handlers
{
    public class UpdateTileImageCollectionHandler : IHandler<UpdateTileImageCollectionMessage>
    {
        private readonly ITileUpdateManager _tileUpdateManager;
        private static readonly List<SearchInstance> _history = new List<SearchInstance>();

        public UpdateTileImageCollectionHandler(ITileUpdateManager tileUpdateManager)
        {
            _tileUpdateManager = tileUpdateManager;
        }

        public void Handle(UpdateTileImageCollectionMessage message)
        {
            if (_history.Contains(message.Instance))
                return;

            var content = TileContentFactory.CreateTileWidePeekImageCollection06();

            content.RequireSquareContent = false;
            content.TextHeadingWrap.Text = "Search for " + message.Instance.Query;
            
            var images = message.Instance.GetRandomImages(6).ToList();

            UpdateImage(content.ImageMain, images[0]);
            UpdateImage(content.ImageSecondary, images[1]);
            UpdateImage(content.ImageSmallColumn1Row1, images[2]);
            UpdateImage(content.ImageSmallColumn1Row2, images[3]);
            UpdateImage(content.ImageSmallColumn2Row1, images[4]);
            UpdateImage(content.ImageSmallColumn2Row2, images[5]);

            _tileUpdateManager.UpdatePrimaryTile(content);

            _history.Add(message.Instance);
        }

        private void UpdateImage(INotificationContentImage imageContent, ImageResult image)
        {
            imageContent.Src = image.Thumbnail.MediaUrl;
            imageContent.Alt = image.Title;
        }
    }
}