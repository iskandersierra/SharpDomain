using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.Processing
{
    public interface ICommandProcessingStep
    {
        void Run(ICommandProcessingContext context);
    }
}
