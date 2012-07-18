using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BingImageSearch.Messaging;

namespace BingImageSearch.Tests.Mocks
{
    public class MockMessageHub : IHub
    {
        public Stack<IMessage> MessageStack { get; set; }

        public MockMessageHub()
        {
            MessageStack = new Stack<IMessage>();
        }

        public async Task Send<TMessage>(TMessage message) where TMessage : IMessage
        {
            MessageStack.Push(message);
            await Task.Run(() => {});
        }
    }
}
