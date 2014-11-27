using System.Collections.Generic;

namespace SharpDomain.Messaging
{
    public interface IEventBus
    {
        void Publish(Envelope<IEvent> eventMessage);
        void Publish(IEnumerable<Envelope<IEvent>> eventMessages);
    }
}
