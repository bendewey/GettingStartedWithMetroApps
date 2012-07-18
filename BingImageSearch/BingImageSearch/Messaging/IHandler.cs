using System.Threading.Tasks;

namespace BingImageSearch.Messaging
{
    public interface IAsyncHandler<TMessage> where TMessage : IMessage
    {
        Task HandleAsync(TMessage message);
    }

    public interface IHandler<TMessage> where TMessage : IMessage
    {
        void Handle(TMessage message);
    }

    public static class HandlerExtensions
    {
        public static Task HandleAsync<TMessage>(this IHandler<TMessage> handler, TMessage message) where TMessage : IMessage
        {
             return Task.Run(() => handler.HandleAsync(message));
        }    
    }
}