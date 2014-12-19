using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.EventSourcing
{
    public interface ICommandProcessor
    {
    }

    public interface ICommandProcessor<TCommand> 
        where TCommand : class, ICommand
    {
        void Process(TCommand command, ICommandProcessorContext context);
    }
}
