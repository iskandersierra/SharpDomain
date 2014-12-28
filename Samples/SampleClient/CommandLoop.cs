using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using SharpDomain.Client;

namespace SampleClient
{
    public class CommandLoop : IWantToRunWhenBusStartsAndStops
    {
        public CommandLoop(CommandSendingLoop loop)
        {
            Loop = loop;
        }

        public CommandSendingLoop Loop { get; set; }

        public void Start()
        {
            Loop.Run();
        }

        public void Stop()
        {
            
        }
    }
}
