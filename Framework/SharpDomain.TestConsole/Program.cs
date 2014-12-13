using System;
using ContactsContext.Commands;
using SharpDomain.EventSourcing;

namespace SharpDomain.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var creator = new DefaultMessageCreator();

            var cmd = creator.CreateMessage<CreateContact>(c =>
                {
                    c.ContactId = new Guid("{4573A8A5-6E33-4BFA-8F5F-4639AAF44A23}");
                    c.Title = "Iskander";
                });

            Console.WriteLine(cmd.ContactId);
            Console.WriteLine(cmd.Title);
        }
    }
}
