using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.Processing
{
    public interface ICommandProcessingPipeline
    {
        void Process(CommandProcessingContext context);
    }
}
