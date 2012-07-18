using System.Collections.Generic;
using System.Linq;
using BingImageSearch.Adapters;
using NotificationsExtensions.ToastContent;

namespace BingImageSearch.Tests.Mocks
{
    public class MockToastNotificationManagerAdapter : IToastNotificationManager
    {
        public List<IToastNotificationContent> Notifications { get; private set; }

        public MockToastNotificationManagerAdapter()
        {
            Notifications = new List<IToastNotificationContent>();
        }

        public void Show(IToastNotificationContent content)
        {
            Notifications.Add(content);
        }

        public TContent Last<TContent>() where TContent : IToastNotificationContent
        {
            return Notifications.OfType<TContent>().LastOrDefault();
        }
    }
}