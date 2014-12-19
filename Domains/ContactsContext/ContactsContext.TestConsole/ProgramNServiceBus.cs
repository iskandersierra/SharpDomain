using System;
using System.Diagnostics;
using ContactsContext.Commands;
using NServiceBus;
using ICommand = SharpDomain.EventSourcing.ICommand;

namespace ContactsContext.TestConsole
{
    partial class Program
    {
        private static void TestNServiceBusClient()
        {
            var busConfiguration = CreateBusConfiguration();

            using (var bus = Bus.Create(busConfiguration))
            {
                bus.Start();
                //Console.WriteLine("Press 'Enter' to send new message.To exit, Ctrl + C");

                var watch = Stopwatch.StartNew();
                for (int i = 0; i < 1000; i++)
                {
                    var iskId = Guid.NewGuid();
                    var iskTitle = "Iskander #" + i;
                    SendCreateContact(bus, iskId, iskTitle);
                }
                Console.WriteLine("Elapsed time: {0}", watch.Elapsed);

            }

            Console.WriteLine("Done! Press any key to exit ...");

            Console.ReadLine();
        }

        private static void SendCreateContact(IStartableBus bus, Guid contactId, string contactTitle)
        {
            //Console.Write(@"Sending CREATE COMMAND for ""{0}""... ", contactTitle);

            //bus.Send("CoreDomains.ContactsContext.Server", 
            //    new CreateContactCommand
            //    {
            //        ContactId = contactId,
            //        Title = contactTitle,
            //    });
            bus.Send<CreateContact>(cc =>
            {
                cc.ContactId = contactId;
                cc.Title = contactTitle;
            });

            //Console.WriteLine("OK!");
        }

        private static BusConfiguration CreateBusConfiguration()
        {
            var busConfiguration = new BusConfiguration();
            //busConfiguration.EndpointName("CoreDomains.ContactsContext.Client");
            busConfiguration.UseSerialization<JsonSerializer>();
            //busConfiguration.AssembliesToScan(typeof(CreateContact).Assembly);
            var conventions = busConfiguration.Conventions();
            var commandType = typeof (ICommand);
            conventions.DefiningCommandsAs(t => t.IsInterface && commandType.IsAssignableFrom(t));
            if (Debugger.IsAttached)
            {
                // For production use please select a durable persistence and script installers
                busConfiguration.UsePersistence<InMemoryPersistence>();
                busConfiguration.EnableInstallers();
            }
            return busConfiguration;
        }
    }

    //public class CreateContactCommand : CreateContact, NServiceBus.ICommand
    //{
    //    public Guid ContactId { get; set; }
    //    public string Title { get; set; }
    //}

}
