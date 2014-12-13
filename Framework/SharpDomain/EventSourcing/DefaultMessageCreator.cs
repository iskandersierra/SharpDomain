using System;
using System.Reflection;
using SharpDomain.Reflection;

namespace SharpDomain.EventSourcing
{
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