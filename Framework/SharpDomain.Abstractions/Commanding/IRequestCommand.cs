using System;

namespace SharpDomain.Commanding
{
    /// <summary>
    /// Represents a domain command to be processed on a server where the requester is expecting some 
    /// response (not necesarilly in a synchronous way). The command Id allow to respond with the given 
    /// id to be correlated by a client expecting the response message
    /// </summary>
    public interface IRequestCommand : IDomainCommand
    {
        Guid CommandId { get; }
    }
}