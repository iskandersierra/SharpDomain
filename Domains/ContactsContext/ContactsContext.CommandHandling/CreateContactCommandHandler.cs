using ContactsContext.Commands;
using ContactsContext.Events;
using SharpDomain.EventSourcing;

namespace ContactsContext.CommandHandling
{
    public class CreateContactCommandHandler
    {
        public void Handle(CreateContact command, ICommandHandlerContext context)
        {
            context.Emmit<ContactCreated>(e =>
            {
                e.ContactId = command.ContactId;
            });
            context.Emmit<ContactTitleUpdated>(e =>
            {
                e.ContactId = command.ContactId;
                e.Title = command.Title;
            });
        }
    }

}
