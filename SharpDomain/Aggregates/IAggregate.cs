using System.Collections.Generic;
using SharpDomain.Messaging;

namespace SharpDomain.Aggregates
{
    /// <summary>
    /// Base interface for aggregate domain objects. It is intended to be merged with IEventSourced 
    /// interface if implementing DDD using CQRS/ES pattern
    /// </summary>
    public interface IAggregate : IEntity
    {
        object Id { get; }

        long Version { get; }

        IEnumerable<IEvent> Events { get; }

        void AppendEvent(IEvent @event);
    }

    public interface IAggregate<TId> : IAggregate
    {
        new TId Id { get; }

        new IEnumerable<IEvent<TId>> Events { get; }

        void AppendEvent(IEvent<TId> @event);

    }
}
