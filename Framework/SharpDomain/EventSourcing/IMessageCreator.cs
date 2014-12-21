using System;

namespace SharpDomain.EventSourcing
{
    public interface IMessageCreator
    {
        Type GetConcreteMessageType(Type messageType);
        Type GetInterfaceMessageType(Type messageType);
    }
}
