using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDomain.Business;

namespace SharpDomain.Processing
{
    public class CommandHandlerProcessingStep : ICommandProcessingStep
    {
        private ICommandHandlerSelector _commandHandlerSelector;

        public void Run(ICommandProcessingContext context)
        {
            var commandHandler = _commandHandlerSelector.SelectFor(context.Command);

            var aggregate = context.Get<IAggregate>();

            commandHandler.Handle(aggregate, context.Command);
        }
    }
}
