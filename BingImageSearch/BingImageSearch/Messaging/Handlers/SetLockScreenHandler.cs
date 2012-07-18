using System;
using System.Threading.Tasks;
using BingImageSearch.Adapters;
using BingImageSearch.Services;
using NotificationsExtensions.ToastContent;

namespace BingImageSearch.Messaging.Handlers
{
    public class SetLockScreenHandler : IAsyncHandler<SetLockScreenMessage>
    {
        private readonly ApplicationSettings _settings;
        private readonly ILockScreen _lockScreen;
        private readonly IStatusService _statusService;

        public SetLockScreenHandler(ApplicationSettings settings, ILockScreen lockScreen, IStatusService statusService)
        {
            _settings = settings;
            _lockScreen = lockScreen;
            _statusService = statusService;
        }

        public async Task HandleAsync(SetLockScreenMessage message)
        {
            var file = await _settings.GetLockScreenFileAsync(message.Image.MediaUrl);
            await _lockScreen.SetImageFileAsync(file);
            _statusService.TemporaryMessage = string.Format("Image {0} set as lock screen.", message.Image.Title);
        }
    }
}