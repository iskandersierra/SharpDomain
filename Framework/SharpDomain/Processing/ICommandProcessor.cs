using System;
using SharpDomain.Messaging;

namespace SharpDomain.Processing
{
    public interface ICommandProcessor
    {
        void Process(ICommand command);
    }

    public class CommandProcessor : ICommandProcessor
    {
        private readonly ICommandProcessingPipelineSelector _pipelineSelector;

        public CommandProcessor(ICommandProcessingPipelineSelector pipelineSelector)
        {
            if (pipelineSelector == null) throw new ArgumentNullException("pipelineSelector");
            _pipelineSelector = pipelineSelector;
        }

        public void Process(ICommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var context = new CommandProcessingContext(command);

            var pipeline = _pipelineSelector.GetPipelineFor(command);
            pipeline.Process(context);
        }
    }

    public interface ICommandProcessingPipelineSelector
    {
        ICommandProcessingPipeline GetPipelineFor(ICommand command);
    }
}
