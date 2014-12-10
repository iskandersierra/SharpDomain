using System;
using SharpDomain.Business;
using SharpDomain.Messaging;

namespace SharpDomain.Processing
{
    public interface ICommandHandler
    {
        void Handle(IAggregate aggregate, ICommand command);
    }

    public interface ICommandHandler<TCommand> : ICommandHandler
        where TCommand : class, ICommand
    {
        void Handle(IAggregate aggregate, TCommand command);
    }
}