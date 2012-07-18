using System.Threading.Tasks;
using Windows.UI.ApplicationSettings;

namespace BingImageSearch.Messaging.Handlers
{
    class ShowSettingsHandler : IHandler<ShowSettingsMessage>
    {
        public void Handle(ShowSettingsMessage message)
        {
            SettingsPane.Show();
        }
    }
}
