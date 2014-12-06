using System.Collections.Generic;
using SharpDomain.Messaging;

namespace SharpDomain.Business
{
    public interface IAggregator
    {
        TAggregate Aggregate<TAggregate>(IEnumerable<IEvent> eventStream) where TAggregate : class, IAggregate;
    }
}