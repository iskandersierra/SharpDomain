namespace SharpDomain.EventSourcing
{
    public interface ICommandProcessor : IMessageProcessor
    {
    }

    public interface ICommandProcessor<in TCommand> 
        where TCommand : class, ICommand
    {
        void Process(TCommand command, ICommandProcessorContext context);
    }
}
