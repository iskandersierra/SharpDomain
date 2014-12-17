using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsContext.Commands;
using Rebus.Configuration;
using Rebus.Logging;
using Rebus.Transports.Msmq;

namespace ContactsContext.TestServer
{
    partial class Program
    {
        private static void TestRebusServer()
        {
            using (var adapter = new BuiltinContainerAdapter())
            {
                adapter.Handle<object>(comm =>
                {
                    Console.WriteLine("Handling message {0}", comm.GetType().FullName);
                });

                var bus = Configure.With(adapter)
                    .Logging(l => l.ColoredConsole(LogLevel.Info))
                    .Transport(t => t.UseMsmqAndGetInputQueueNameFromAppConfig())
                    .CreateBus()
                    .Start();

                Console.WriteLine("Listening for commands ... ");

                Console.ReadLine();
            }

        }
    }
}
