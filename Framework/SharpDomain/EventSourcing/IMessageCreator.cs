using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SharpDomain.Reflection;

namespace SharpDomain.EventSourcing
{
    public interface IMessageCreator
    {
        Type GetConcreteMessageType(Type messageType);
        Type GetInterfaceMessageType(Type messageType);
    }

    public static class ExtensionsToMessageCreator
    {
        public static Type GetInterfaceMessageType(this IMessageCreator creator, object message)
        {
            var interfaceType = creator.GetInterfaceMessageType(message.GetType());
            return interfaceType;
        }
        public static object CreateMessage(this IMessageCreator creator, Type messageType)
        {
            var concreteType = creator.GetConcreteMessageType(messageType);
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

    public class DefaultMessageCreator : IMessageCreator
    {
        private DaoInterfaceProxyBuilder proxyBuilder = new DaoInterfaceProxyBuilder();

        public Type GetConcreteMessageType(Type messageType)
        {
            if (messageType == null) throw new ArgumentNullException("messageType");
            if (messageType.IsClass)
                return messageType;
            if (messageType.IsInterface)
                return proxyBuilder.GetConcreteType(messageType);
            throw new ArgumentException("Invalid message type: " + messageType.FullName);
        }

        public Type GetInterfaceMessageType(Type messageType)
        {
            if (messageType == null) throw new ArgumentNullException("messageType");
            var attr = messageType.GetCustomAttribute<DaoInterfaceProxyForAttribute>();
            if (attr != null)
                return attr.DaoInterfaceType;
            return messageType;
        }
    }
}
