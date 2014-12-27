using SharpDomain.Commanding;

namespace SharpDomain.Validation
{
    public interface ICommandValidator
    {
        CommandResult Validate(IDomainCommand command);
    }
}
