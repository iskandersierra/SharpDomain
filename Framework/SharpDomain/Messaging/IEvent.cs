using System;

namespace SharpDomain.Messaging
{
    public interface IEvent
    {
        Guid SourceId { get; }

        int Version { get; }
    }
}
