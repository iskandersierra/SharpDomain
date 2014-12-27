using System;

namespace SharpDomain.Eventing.ServiceModel.Bus
{
    public interface IPublishableEvent : IEvent
    {
        Guid EventSourceId { get; }

        long EventSequence { get; }

        Guid CommitId { get; }

        object Payload { get; }
    }
}