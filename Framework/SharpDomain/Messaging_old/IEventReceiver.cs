namespace SharpDomain.Messaging
{
    public interface IEventReceiver
    {
        void RecieveEvent(IDomainEvent @event);
    }
}