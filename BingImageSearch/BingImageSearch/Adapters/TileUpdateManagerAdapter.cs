using NotificationsExtensions.TileContent;
using Windows.UI.Notifications;

namespace BingImageSearch.Adapters
{
    public class TileUpdateManagerAdapter : ITileUpdateManager
    {
        public void UpdatePrimaryTile(ITileNotificationContent content)
        {
            var notification = content.CreateNotification();
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }
    }
}