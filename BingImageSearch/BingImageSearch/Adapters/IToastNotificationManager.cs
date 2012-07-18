using NotificationsExtensions.ToastContent;

namespace BingImageSearch.Adapters
{
    public interface IToastNotificationManager
    {
        void Show(IToastNotificationContent content);
    }
}
