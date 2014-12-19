//using System;
//using System.Diagnostics;
//using ContactsContext.Commands;
//using MassTransit;

//namespace ContactsContext.TestConsole
//{
//    partial class Program
//    {
//        private static void TestMassTransitClient()
//        {
//            var bus = ServiceBusFactory.New(sbc =>
//            {
//                sbc.UseMsmq(msmq =>
//                {
//                    msmq.VerifyMsmqConfiguration();
//                });
//            });

//            var watch = Stopwatch.StartNew();

//            for (int i = 0; i < 1000; i++)
//            {
//                var iskId = Magnum.CombGuid.Generate();
//                var iskTitle = "Iskander #" + i;
//                SendCreateContact(bus, iskId, iskTitle);
//            }

//            Console.WriteLine("Elapsed time: {0}", watch.Elapsed);

//            Console.WriteLine("Done! Press any key to exit ...");

//            Console.ReadLine();
//        }

//        private static void SendCreateContact(IServiceBus bus, Guid contactId, string contactTitle)
//        {
//            Console.Write(@"Sending CREATE COMMAND for ""{0}""... ", contactTitle);

//            bus.Publish<CreateContact>(new CreateContactCommand
//            {
//                ContactId = contactId,
//                Title = contactTitle,
//            });

//            Console.WriteLine("OK!");
//        }

//        //private static BusConfiguration CreateBusConfiguration()
//        //{
//        //    var busConfiguration = new BusConfiguration();
//        //    //busConfiguration.EndpointName("CoreDomains.ContactsContext.Client");
//        //    busConfiguration.UseSerialization<JsonSerializer>();
//        //    //busConfiguration.AssembliesToScan(typeof(CreateContact).Assembly);
//        //    var conventions = busConfiguration.Conventions();
//        //    var commandType = typeof(ICommand);
//        //    conventions.DefiningCommandsAs(t => t.IsInterface && commandType.IsAssignableFrom(t));
//        //    if (Debugger.IsAttached)
//        //    {
//        //        // For production use please select a durable persistence and script installers
//        //        busConfiguration.UsePersistence<InMemoryPersistence>();
//        //        busConfiguration.EnableInstallers();
//        //    }
//        //    return busConfiguration;
//        //}
//    }
//}
