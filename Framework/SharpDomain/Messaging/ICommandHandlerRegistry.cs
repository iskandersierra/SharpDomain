namespace SharpDomain.Messaging
{
    public interface ICommandHandlerRegistry
    {
        void Register(ICommandHandler handler);
    }
}