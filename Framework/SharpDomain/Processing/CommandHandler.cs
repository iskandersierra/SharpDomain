using SharpDomain.Business;
using SharpDomain.Messaging;

namespace SharpDomain.Processing
{
    public abstract class CommandHandler<TCommand> : 
        ICommandHandler<TCommand> 
        where TCommand : class, ICommand
    {
        public abstract void Handle(IAggregate aggregate, TCommand command);

        void ICommandHandler.Handle(IAggregate aggregate, ICommand command)
        {
            Handle(aggregate, (TCommand) command);
        }
    }
}