namespace SharpDomain.Messaging
{
    public interface IEnvelopedEventHandler<TEvent> : IEventHandler
        where TEvent : IEvent
    {
        void Handle(Envelope<TEvent> envelope);
    }
}