namespace SharpDomain.Messaging
{
    public interface IEvent
    {
        object SourceId { get; }

        long Version { get; }
    }

    public interface IEvent<TSourceId> : IEvent
    {
        new TSourceId SourceId { get; }
    }
}
