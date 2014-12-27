using System;

namespace SharpDomain.Messaging
{
    /// <summary>
    /// Message sent as a response to an aggregate creation command
    /// </summary>
    public interface IAggregateCreatedMessage : ICommandResponseMessage
    {
        /// <summary>
        /// Aggregate id given to the created instance
        /// </summary>
        Guid AggregateId { get; set; }
    }
}