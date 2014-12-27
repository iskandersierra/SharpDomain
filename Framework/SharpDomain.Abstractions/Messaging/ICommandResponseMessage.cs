using System;

namespace SharpDomain.Messaging
{
    /// <summary>
    /// Represents a messase as a response to a IRequestCommand
    /// </summary>
    public interface ICommandResponseMessage : IDomainMessage
    {
        /// <summary>
        /// Id of command correlated to this response
        /// </summary>
        Guid CommandId { get; set; }
    }
}
