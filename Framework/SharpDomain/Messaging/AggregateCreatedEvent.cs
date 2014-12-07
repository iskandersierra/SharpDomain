using System;
using SharpDomain.Properties;

namespace SharpDomain.Messaging
{
    public abstract class AggregateCreatedEvent : Event, IAggregateCreatedEvent
    {
        protected AggregateCreatedEvent(Guid sourceId) : base(sourceId, 1)
        {
        }

        protected override void UpdateEvent(Guid sourceId, int version)
        {
            if (version != 1)
                throw new ArgumentOutOfRangeException(Resources.CreationEventVersionMustBeOne);
            base.UpdateEvent(sourceId, 1);
        }
    }
}