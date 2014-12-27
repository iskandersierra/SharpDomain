using System.Collections.Generic;
using SharpDomain.EventSourcing;

namespace SharpDomain.Business
{
    public interface IAggregator
    {
        TAggregate Aggregate<TAggregate>(IEnumerable<IEvent> eventStream) where TAggregate : class, IAggregate;
    }
}