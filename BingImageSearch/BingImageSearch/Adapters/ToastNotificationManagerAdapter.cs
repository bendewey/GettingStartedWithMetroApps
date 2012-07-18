using NotificationsExtensions.ToastContent;
using Windows.UI.Notifications;

namespace BingImageSearch.Adapters
{
    public class ToastNotificationManagerAdapter : IToastNotificationManager
    {
        public void Show(IToastNotificationContent content)
        {
            var notification = content.CreateNotification();
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }
    }
}