
namespace SharpDomain.Messaging
{
    public interface ICommandReceiver
    {
        void RecieveCommand(IDomainCommand command);
    }
}
