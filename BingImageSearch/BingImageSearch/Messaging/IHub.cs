using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingImageSearch.Messaging
{
    public interface IHub
    {
        Task Send<TMessage>(TMessage message) where TMessage : IMessage;
    }
}
