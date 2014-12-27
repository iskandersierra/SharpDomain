using SharpDomain.Validation;

namespace SharpDomain.Messaging
{

    /// <summary>
    /// Message sent as a response to a completed command
    /// </summary>
    public interface ICommandCompletedMessage : ICommandResponseMessage
    {
        CommandResult Result { get; set; }
    }

}