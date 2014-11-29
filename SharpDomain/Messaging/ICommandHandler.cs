namespace SharpDomain.Messaging
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<TCommand> : ICommandHandler
        where TCommand : ICommand
    {
        void HandleCommand(TCommand @Command);
    }
}