//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Messaging;
//using System.Text;
//using System.Threading.Tasks;
//using MassTransit;

//namespace ContactsContext.TestServer
//{
//    partial class Program
//    {
//        private static void TestMassTransitServer()
//        {
//            //var queuePath = @".\Private$\TestMassTransitServer";
//            //var queue = MessageQueue.Exists(queuePath) 
//            //    ? new MessageQueue(queuePath)
//            //    : MessageQueue.Create(queuePath, true);

//            var bus = ServiceBusFactory.New(sbc =>
//            {
//                sbc.UseMsmq(msmq =>
//                {
//                    msmq.VerifyMsmqConfiguration();
//                    msmq.UseMulticastSubscriptionClient();
//                });
//                sbc.ReceiveFrom("msmq://localhost/TestMassTransitServer");
//                sbc.SetCreateMissingQueues(true);
//                sbc.SetCreateTransactionalQueues(true);
//            });
//            bus.Probe();
//            bus.WriteIntrospectionToConsole();

//            bus.SubscribeHandler<object>(obj =>
//            {
//                Console.WriteLine("Handling message {0}", obj.GetType().FullName);
//            });

//            Console.WriteLine("Listening for commands ... ");

//            Console.ReadLine();
//            bus.Dispose();
//        }
//    }
//}
