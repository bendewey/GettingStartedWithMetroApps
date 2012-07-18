using BingImageSearch.Messaging;
using BingImageSearch.Services;
using BingImageSearch.Tests.Mocks;

namespace BingImageSearch.Tests.ViewModels
{
    public class ViewModelScenarioBase
    {
        public MockApplicationSettings ApplicationSettings;
        public MockNavigationService NavigationService;
        public MockMessageHub MessageHub;

        public ViewModelScenarioBase()
        {
            MessageHub = new MockMessageHub();
            ApplicationSettings = new MockApplicationSettings();
            NavigationService = new MockNavigationService();
        }

        public TMessage Message<TMessage>() where TMessage : class, IMessage
        {
            return MessageHub.MessageStack.Pop() as TMessage;
        }

        public bool MessageSent<TMessage>() where TMessage : class, IMessage
        {
            return (MessageHub.MessageStack.Pop() as TMessage) != null;
        }

        public bool NavigatedTo<T>()
        {
            return NavigationService.NavigationStack.Pop().Source == typeof(T);
        }
    }
}
