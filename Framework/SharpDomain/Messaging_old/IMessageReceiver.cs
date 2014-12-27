namespace SharpDomain.Messaging
{
    public interface IMessageReceiver
    {
        void RecieveMessage(IDomainMessage message);
        
    }
}