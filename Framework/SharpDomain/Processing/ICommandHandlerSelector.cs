using SharpDomain.Messaging;

namespace SharpDomain.Processing
{
    public interface ICommandHandlerSelector
    {
        ICommandHandler SelectFor(ICommand command);
    }
}