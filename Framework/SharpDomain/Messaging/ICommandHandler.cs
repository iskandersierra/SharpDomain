namespace SharpDomain.Messaging
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<TCommand> : ICommandHandler
        where TCommand : class, ICommand
    {
        void HandleCommand(TCommand command);
    }
}