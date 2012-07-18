using System.Collections.Generic;
using System.Linq;
using BingImageSearch.Adapters;
using NotificationsExtensions.TileContent;

namespace BingImageSearch.Tests.Mocks
{
    public class MockTileUpdateManagerAdapter : ITileUpdateManager
    {
        public List<ITileNotificationContent> TileUpdates { get; private set; }

        public MockTileUpdateManagerAdapter()
        {
            TileUpdates = new List<ITileNotificationContent>();
        }

        public void UpdatePrimaryTile(ITileNotificationContent content)
        {
            TileUpdates.Add(content);
        }

        public TContent Last<TContent>() where TContent : ITileNotificationContent
        {
            return TileUpdates.OfType<TContent>().LastOrDefault();
        }
    }
}