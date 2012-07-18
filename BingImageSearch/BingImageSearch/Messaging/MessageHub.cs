using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MetroIoc;
using Windows.UI.Popups;

namespace BingImageSearch.Messaging
{
    public class MessageHub : IHub
    {
        private readonly IContainer _container;

        public MessageHub(IContainer container)
        {
            _container = container;
        }

        public async Task Send<TMessage>(TMessage message) where TMessage : IMessage
        {
            var handler = _container.TryResolve<IHandler<TMessage>>(null);
            if (handler != null)
            {
                handler.Handle(message);
                return;
            }

            var asyncHandler = _container.TryResolve<IAsyncHandler<TMessage>>(null);
            if (asyncHandler != null)
            {
                await asyncHandler.HandleAsync(message);
                return;
            }
        }
    }
}
