using System;

namespace SharpDomain.EventSourcing
{
    public interface IEvent
    {
        Guid SourceId { get; set; }

        int Version { get; set; }
    }
}
