using ContactsContext.Commands;
using ContactsContext.Events;
using SharpDomain.EventSourcing;

namespace ContactsContext.EventSourcing
{
    public class CreateContactCommandHandler
    {
        public void Handle(CreateContact command, ICommandHandlerContext context)
        {
            context.Emmit<ContactCreated>(e =>
            {
                e.SourceId = command.ContactId;
            });
            context.Emmit<ContactTitleUpdated>(e =>
            {
                e.SourceId = command.ContactId;
                e.Title = command.Title;
            });
        }
    }
    public class UpdateContactTitleCommandHandler
    {
        public void Handle(UpdateContactTitle command, ICommandHandlerContext context)
        {
            context.Emmit<ContactTitleUpdated>(e =>
            {
                e.SourceId = command.ContactId;
                e.Title = command.Title;
            });
        }
    }
    public class UpdateContactPictureCommandHandler
    {
        public void Handle(UpdateContactPicture command, ICommandHandlerContext context)
        {
            context.Emmit<ContactPictureUpdated>(e =>
            {
                e.SourceId = command.ContactId;
                e.PictureId = command.PictureId;
            });
        }
    }
    public class ClearContactPictureCommandHandler
    {
        public void Handle(ClearContactPicture command, ICommandHandlerContext context)
        {
            context.Emmit<ContactPictureCleared>(e =>
            {
                e.SourceId = command.ContactId;
            });
        }
    }

}
