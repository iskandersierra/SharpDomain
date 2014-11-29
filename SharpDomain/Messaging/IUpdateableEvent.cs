namespace SharpDomain.Messaging
{
    public interface IUpdateableEvent : IEvent
    {
        void UpdateEvent(object sourceId, long version);
    }
}