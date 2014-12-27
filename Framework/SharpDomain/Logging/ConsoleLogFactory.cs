using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.Logging
{
    public class ConsoleLogFactory : SimpleLogFactoryBase
    {
        protected override ILog CreateNamedLog(string name)
        {
            return new ConsoleLog(this, name);
        }

        protected class ConsoleLog : SimpleLogBase<ConsoleLogFactory>
        {
            public ConsoleLog(ConsoleLogFactory factory, string name) : base(factory, name)
            {
                
            }

            protected override void LogInternal(LoggingLevel level, string message)
            {
                Console.WriteLine(message);
            }
        }
    }
}
