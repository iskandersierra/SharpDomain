using System;
using System.Collections.Generic;

namespace SharpDomain.Commanding
{

    /// <summary>
    /// Allows to intercept sending commands to current implementation to do differen kinds of preprocessing like:
    /// - Adding standard headers like user-id/role/name/email/type, environment-id/project/website/area
    /// - Validating command attributes
    /// - Checking access control of current user on command/environment
    /// </summary>
    public abstract class CommandBusDecorator : ICommandBus
    {
        protected ICommandBus CommandBusImplementation { get; set; }

        public CommandBusDecorator(ICommandBus commandBusImplementation)
        {
            CommandBusImplementation = commandBusImplementation;
        }

        public ISendCommandCallback Send(IDomainCommand command, Action<IDictionary<string, string>> configHeaders = null)
        {
            return CommandBusImplementation.Send(command, configHeaders: configHeaders);
        }

        public ISendCommandCallback Send<TCommand>(Action<TCommand> initCommand, Action<IDictionary<string, string>> configHeaders = null) 
            where TCommand : class, IDomainCommand
        {
            return CommandBusImplementation.Send(initCommand, configHeaders: configHeaders);
        }
    }
}
