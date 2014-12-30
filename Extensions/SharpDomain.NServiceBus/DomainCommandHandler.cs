using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using NServiceBus;
using SharpDomain.Commanding;

namespace SharpDomain.NServiceBus
{
    public class DomainCommandHandler : 
        IHandleMessages<IDomainCommand>
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        protected IBus Bus { get; set; }

        public void Handle(IDomainCommand command)
        {
            Log.TraceFormat("Handling command of type {0}", command.GetType().FullName);
        }
    }
}
