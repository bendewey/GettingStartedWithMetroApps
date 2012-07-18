using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using BingImageSearch.Messaging;

namespace BingImageSearch.Tests.Messaging
{
    public class HandlerScenario
    {
        public object Handler { get; set; }

        public void WhenHandling<TMessage>() where TMessage : IMessage, new()
        {
            WhenHandling(() => new TMessage());
        }

        public async Task WhenHandlingAsync<TMessage>(Func<TMessage> messageFactory) where TMessage : IMessage
        {
            if (Handler == null)
            {
                throw new Exception("No handler defined.");
            }

            TMessage message = messageFactory();

            var handlerType = Handler.GetType().GetTypeInfo();
            var method = (from m in handlerType.DeclaredMethods
                          let firstParam = m.GetParameters().FirstOrDefault()
                          where (m.Name == "Handle" || m.Name == "HandleAsync")
                          && firstParam != null && firstParam.ParameterType == typeof(TMessage)
                          select m).FirstOrDefault();

            if (method == null)
            {
                throw new Exception(string.Format("No HandleAsync({0} message) method found on hander {1}", typeof(TMessage).Name, handlerType.Name));
            }
            if (method.ReturnType == typeof(Task))
            {
                await (Task)method.Invoke(Handler, new object[] { message });
            }
            else
            {
                method.Invoke(Handler, new object[] { message });
            }
        }

        public void WhenHandling<TMessage>(Func<TMessage> messageFactory) where TMessage : IMessage
        {
            var task = WhenHandlingAsync(messageFactory);
            task.Wait();
        }
    }
}
