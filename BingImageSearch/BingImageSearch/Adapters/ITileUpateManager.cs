using NotificationsExtensions.TileContent;

namespace BingImageSearch.Adapters
{
    public interface ITileUpdateManager
    {
        void UpdatePrimaryTile(ITileNotificationContent content);
    }
}
