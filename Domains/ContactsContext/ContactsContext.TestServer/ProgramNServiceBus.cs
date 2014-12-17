using System;
using System.Diagnostics;
using ContactsContext.Commands;
using NServiceBus;
using ICommand = SharpDomain.EventSourcing.ICommand;

namespace ContactsContext.TestServer
{
    partial class Program
    {
        private static void TestNServiceBusServer()
        {
            var busConfiguration = CreateBusConfiguration();

            using (var bus = Bus.Create(busConfiguration))
            {
                bus.Start();
                //Console.WriteLine("Press 'Enter' to send new message.To exit, Ctrl + C");

                Console.WriteLine("To exit, Ctrl + C");

                Console.ReadLine();
            }
        }

        private static BusConfiguration CreateBusConfiguration()
        {
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("CoreDomains.ContactsContext.Server");
            busConfiguration.UseSerialization<JsonSerializer>();
            var conventions = busConfiguration.Conventions();
            var commandType = typeof(ICommand);
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

    public class NSBCreateContactHandler : IHandleMessages<object>
    {
        public void Handle(object message)
        {
            Console.WriteLine("Handling message {0}", message.GetType().FullName);
            //Console.WriteLine("Handling message CreateContact({0}, {1})", message.ContactId, message.Title);
        }
    }
}
