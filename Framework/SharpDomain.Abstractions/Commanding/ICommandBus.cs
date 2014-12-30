using System;
using System.Collections.Generic;

namespace SharpDomain.Commanding
{
    /// <summary>
    /// Service to send command to a target server or distributor
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// Sends the command to a target server or distributor
        /// </summary>
        /// <param name="command"></param>
        /// <param name="configHeaders"></param>
        ISendCommandCallback Send(IDomainCommand command, Action<IDictionary<string, string>> configHeaders = null);

        ISendCommandCallback Send<TCommand>(Action<TCommand> initCommand, Action<IDictionary<string, string>> configHeaders = null)
            where TCommand : class, IDomainCommand;
    }
}
