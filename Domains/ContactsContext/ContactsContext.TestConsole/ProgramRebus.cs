using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsContext.Commands;
using Rebus;
using Rebus.Configuration;
using Rebus.Logging;
using Rebus.Transports.Msmq;

namespace ContactsContext.TestConsole
{
    partial class Program
    {
        private static void TestRebusClient()
        {
            using(var adapter = new BuiltinContainerAdapter())
            {
                var bus = Configure.With(adapter)
                    .Logging(l => l.ColoredConsole(LogLevel.Info))
                    .MessageOwnership(o => o.FromRebusConfigurationSection())
                    .Transport(t => t.UseMsmqInOneWayClientMode())
                    .CreateBus()
                    .Start();

                var watch = Stopwatch.StartNew();
                
                for (int i = 0; i < 1000; i++)
                {
                    var iskId = Magnum.CombGuid.Generate();
                    var iskTitle = "Iskander #" + i;
                    SendCreateContact(bus, iskId, iskTitle);
                }

                Console.WriteLine("Elapsed time: {0}", watch.Elapsed);
            }

            Console.WriteLine("Done! Press any key to exit ...");

            Console.ReadLine();
        }

        private static void SendCreateContact(IBus bus, Guid contactId, string contactTitle)
        {
            Console.Write(@"Sending CREATE COMMAND for ""{0}""... ", contactTitle);

            bus.Send(new CreateContactCommand
            {
                ContactId = contactId,
                Title = contactTitle,
            });

            Console.WriteLine("OK!");
        }

        //private static BusConfiguration CreateBusConfiguration()
        //{
        //    var busConfiguration = new BusConfiguration();
        //    //busConfiguration.EndpointName("CoreDomains.ContactsContext.Client");
        //    busConfiguration.UseSerialization<JsonSerializer>();
        //    //busConfiguration.AssembliesToScan(typeof(CreateContact).Assembly);
        //    var conventions = busConfiguration.Conventions();
        //    var commandType = typeof(ICommand);
        //    conventions.DefiningCommandsAs(t => t.IsInterface && commandType.IsAssignableFrom(t));
        //    if (Debugger.IsAttached)
        //    {
        //        // For production use please select a durable persistence and script installers
        //        busConfiguration.UsePersistence<InMemoryPersistence>();
        //        busConfiguration.EnableInstallers();
        //    }
        //    return busConfiguration;
        //}
    }
}
