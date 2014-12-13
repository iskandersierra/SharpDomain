using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.EventSourcing
{
    public interface IMessageCreator
    {
        Type GetMessageType(Type messageType);
    }

    public static class ExtensionsToMessageCreator
    {
        public static object CreateMessage(this IMessageCreator creator, Type messageType)
        {
            var concreteType = creator.GetMessageType(messageType);
            var message = Activator.CreateInstance(concreteType);
            return message;
        }
        public static TMessage CreateMessage<TMessage>(this IMessageCreator creator) where TMessage : class
        {
            var message = (TMessage)creator.CreateMessage(typeof (TMessage));
            return message;
        }

        public static T CreateMessage<T>(this IMessageCreator creator, Action<T> initialize) where T : class
        {
            var message = creator.CreateMessage<T>();
            initialize(message);
            return message;
        }

    }
}
