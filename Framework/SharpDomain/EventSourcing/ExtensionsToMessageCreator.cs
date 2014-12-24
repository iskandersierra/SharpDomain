using System;

namespace SharpDomain.EventSourcing
{
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
        public static TMessage CreateMessage<TMessage>(this IMessageCreator creator) where TMessage : class, IMessage
        {
            var message = (TMessage)creator.CreateMessage(typeof (TMessage));
            return message;
        }
        public static T CreateMessage<T>(this IMessageCreator creator, Action<T> initialize) where T : class, IMessage
        {
            var message = creator.CreateMessage<T>();
            initialize(message);
            return message;
        }

    }
}