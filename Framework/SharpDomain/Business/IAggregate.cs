using System;
using System.Collections.Generic;
using SharpDomain.EventSourcing;

namespace SharpDomain.Business
{
    /// <summary>
    /// Base interface for aggregate domain objects. Aggregate represent entities (with identity)
    /// responsible for managing transient instance state and events, and for giving enough joint points
    /// for other transversal services to cut and control system behaviour like validation, security 
    /// and access control, event transmission, logging, projection into view, etc.
    /// </summary>
    public interface IAggregate
    {
        Guid Id { get; }

        int Version { get; }

        void ApplyEvent(IEvent @event);

        IEnumerable<IEvent> UncommittedEvents { get; }
    }

    public interface IAggregateWithCommittedEvents : IAggregate
    {
        IEnumerable<IEvent> CommittedEvents { get; }
    }
}
