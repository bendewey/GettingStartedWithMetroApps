using System.Linq;
using System.Threading.Tasks;
using BingImageSearch.Adapters;
using BingImageSearch.Services;
using NotificationsExtensions.TileContent;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace BingImageSearch.Messaging.Handlers
{
    public class UpdateTileHandler : IHandler<UpdateTileMessage>
    {
        private readonly ITileUpdateManager _tileUpdateManager;
        private readonly IStatusService _statusService;

        public UpdateTileHandler(ITileUpdateManager tileUpdateManager, IStatusService statusService)
        {
            _tileUpdateManager = tileUpdateManager;
            _statusService = statusService;
        }

        public void Handle(UpdateTileMessage message)
        {
            var url = message.Image.MediaUrl;
            if (message.Image.Width > 800 || message.Image.Height > 800)
            {
                // after much testing it appears that images > 800px cannot be used as tiles
                url = message.Image.Thumbnail.MediaUrl;
            }

            var content = TileContentFactory.CreateTileWidePeekImageAndText01();
            content.TextBodyWrap.Text = message.Image.Title;
            content.Image.Src = url;
            content.Image.Alt = message.Image.Title;
            
            // Square image substitute
            var squareContent = TileContentFactory.CreateTileSquareImage();
            squareContent.Image.Src = url;
            squareContent.Image.Alt = message.Image.Title;

            content.SquareContent = squareContent;

            _tileUpdateManager.UpdatePrimaryTile(content);

            _statusService.TemporaryMessage = "Tile Update sent for " + message.Image.Title;
        }
    }
}
