using ContactsContext.Commands;
using ContactsContext.Events;
using SharpDomain.EventSourcing;

namespace ContactsContext.EventSourcing
{
    public class ContactCommandProcessor : 
        ICommandProcessor<CreateContact>,
        ICommandProcessor<UpdateContactTitle>,
        ICommandProcessor<UpdateContactPicture>,
        ICommandProcessor<ClearContactPicture>
    {
        public void Process(CreateContact command, ICommandProcessorContext context)
        {
            context.Emmit<ContactCreated>();
            context.Emmit<ContactTitleUpdated>(e => { e.Title = command.Title; });
        }

        public void Process(UpdateContactTitle command, ICommandProcessorContext context)
        {
            context.Emmit<ContactTitleUpdated>(e => { e.Title = command.Title; });
        }

        public void Process(UpdateContactPicture command, ICommandProcessorContext context)
        {
            context.Emmit<ContactPictureUpdated>(e => { e.PicturePath = command.PicturePath; });
        }

        public void Process(ClearContactPicture command, ICommandProcessorContext context)
        {
            context.Emmit<ContactPictureCleared>();
        }
    }
}
