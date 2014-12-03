using System;

namespace SharpDomain.Messaging
{
    public interface IUpdateableEvent : IEvent
    {
        void UpdateEvent(Guid sourceId, int version);
    }
}