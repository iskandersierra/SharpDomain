using System;
using ContactsContext.Commands;
using ContactsContext.Events;
using NUnit.Framework;
using SharpDomain.EventSourcing;
using TechTalk.SpecFlow;

namespace ContactsContext.EventSourcing.Specs
{
    [Binding]
    public class ContactSteps
    {
        private static readonly DefaultMessageCreator MessageCreator = new DefaultMessageCreator();

        #region [ Given command handlers ]
        [Given(@"a create contact command handler")]
        public void GivenACreateContactCommandHandler()
        {
            var handler = new CreateContactCommandHandler();

            ScenarioContext.Current.Set(handler);
        }
        [Given(@"a update contact title command handler")]
        public void GivenAUpdateContactTitleCommandHandler()
        {
            var handler = new UpdateContactTitleCommandHandler();

            ScenarioContext.Current.Set(handler);
        }
        [Given(@"a update contact picture command handler")]
        public void GivenAUpdateContactPictureCommandHandler()
        {
            var handler = new UpdateContactPictureCommandHandler();

            ScenarioContext.Current.Set(handler);
        }
        [Given(@"a clear contact picture command handler")]
        public void GivenAClearContactPictureCommandHandler()
        {
            var handler = new ClearContactPictureCommandHandler();

            ScenarioContext.Current.Set(handler);
        }
        #endregion [ Given command handlers ]

        #region [ Given command handler context ]
        [Given(@"a command handler context")]
        public void GivenACommandHandlerContext()
        {
            var context = new CommandHandlerContextMock();
            ScenarioContext.Current.Set(context);
        }
        #endregion [ Given command handler context ]

        #region [ Given commands ]
        [Given(@"a create contact command is created with ""(.*)"" and ""(.*)""")]
        public void GivenACreateContactCommandIsCreatedWithAnd(Guid contactId, string title)
        {
            var command = MessageCreator.CreateMessage<CreateContact>(c =>
            {
                c.ContactId = contactId;
                c.Title = title;
            });
            ScenarioContext.Current.Set(command);
        }
        [Given(@"a update contact title command is created with ""(.*)"" and ""(.*)""")]
        public void GivenAUpdateContactTitleCommandIsCreatedWithAnd(Guid contactId, string title)
        {
            var command = MessageCreator.CreateMessage<UpdateContactTitle>(c =>
            {
                c.ContactId = contactId;
                c.Title = title;
            });
            ScenarioContext.Current.Set(command);
        }
        [Given(@"a update contact picture command is created with ""(.*)"" and ""(.*)""")]
        public void GivenAUpdateContactPictureCommandIsCreatedWithAnd(Guid contactId, Guid pictureId)
        {
            var command = MessageCreator.CreateMessage<UpdateContactPicture>(c =>
            {
                c.ContactId = contactId;
                c.PictureId = pictureId;
            });
            ScenarioContext.Current.Set(command);
        }
        [Given(@"a clear contact picture command is created with ""(.*)""")]
        public void GivenAClearContactPictureCommandIsCreatedWith(Guid contactId)
        {
            var command = MessageCreator.CreateMessage<ClearContactPicture>(c =>
            {
                c.ContactId = contactId;
            });
            ScenarioContext.Current.Set(command);
        }
        #endregion [ Given commands ]

        #region [ When handling commands ]
        [When(@"the create contact command handler handles the command")]
        public void WhenTheCreateContactCommandHandlerHandlesTheCommand()
        {
            var context = ScenarioContext.Current.Get<CommandHandlerContextMock>();
            var command = ScenarioContext.Current.Get<CreateContact>();
            var handler = ScenarioContext.Current.Get<CreateContactCommandHandler>();

            handler.Handle(command, context);
        }
        [When(@"the update contact title command handler handles the command")]
        public void WhenTheUpdateContactTitleCommandHandlerHandlesTheCommand()
        {
            var context = ScenarioContext.Current.Get<CommandHandlerContextMock>();
            var command = ScenarioContext.Current.Get<UpdateContactTitle>();
            var handler = ScenarioContext.Current.Get<UpdateContactTitleCommandHandler>();

            handler.Handle(command, context);
        }
        [When(@"the update contact picture command handler handles the command")]
        public void WhenTheUpdateContactPictureCommandHandlerHandlesTheCommand()
        {
            var context = ScenarioContext.Current.Get<CommandHandlerContextMock>();
            var command = ScenarioContext.Current.Get<UpdateContactPicture>();
            var handler = ScenarioContext.Current.Get<UpdateContactPictureCommandHandler>();

            handler.Handle(command, context);
        }
        [When(@"the clear contact picture command handler handles the command")]
        public void WhenTheClearContactPictureCommandHandlerHandlesTheCommand()
        {
            var context = ScenarioContext.Current.Get<CommandHandlerContextMock>();
            var command = ScenarioContext.Current.Get<ClearContactPicture>();
            var handler = ScenarioContext.Current.Get<ClearContactPictureCommandHandler>();

            handler.Handle(command, context);
        }
        #endregion [ When handling commands ]

        #region [ Then ]
        [Then(@"the command handler context has (.*) emmitted events")]
        public void ThenTheCommandHandlerContextHasEmmittedEvents(int eventCount)
        {
            var context = ScenarioContext.Current.Get<CommandHandlerContextMock>();

            Assert.That(context.EmmittedEvents.Count, Is.EqualTo(eventCount));
        }

        [Then(@"the command handler context has a contact created event as event (.*) with ""(.*)""")]
        public void ThenTheCommandHandlerContextHasAContactCreatedEventAsEventWith(int pos, Guid commandId)
        {
            var context = ScenarioContext.Current.Get<CommandHandlerContextMock>();

            var @event = context.EmmittedEvents[pos - 1] as ContactCreated;
            Assert.That(@event, Is.Not.Null);
            Assert.That(@event.SourceId, Is.EqualTo(commandId));
        }

        [Then(@"the command handler context has a contact title updated event as event (.*) with ""(.*)"" and ""(.*)""")]
        public void ThenTheCommandHandlerContextHasAContactTitleUpdatedEventAsEventWithAnd(int pos, Guid commandId, string title)
        {
            var context = ScenarioContext.Current.Get<CommandHandlerContextMock>();

            var @event = context.EmmittedEvents[pos - 1] as ContactTitleUpdated;
            Assert.That(@event, Is.Not.Null);
            Assert.That(@event.SourceId, Is.EqualTo(commandId));
            Assert.That(@event.Title, Is.EqualTo(title));
        }
        [Then(@"the command handler context has a contact picture updated event as event (.*) with ""(.*)"" and ""(.*)""")]
        public void ThenTheCommandHandlerContextHasAContactPictureUpdatedEventAsEventWithAnd(int pos, Guid commandId, Guid pictureId)
        {
            var context = ScenarioContext.Current.Get<CommandHandlerContextMock>();

            var @event = context.EmmittedEvents[pos - 1] as ContactPictureUpdated;
            Assert.That(@event, Is.Not.Null);
            Assert.That(@event.SourceId, Is.EqualTo(commandId));
            Assert.That(@event.PictureId, Is.EqualTo(pictureId));
        }
        [Then(@"the command handler context has a contact picture cleared event as event (.*) with ""(.*)""")]
        public void ThenTheCommandHandlerContextHasAContactPictureClearedEventAsEventWith(int pos, Guid commandId)
        {
            var context = ScenarioContext.Current.Get<CommandHandlerContextMock>();

            var @event = context.EmmittedEvents[pos - 1] as ContactPictureCleared;
            Assert.That(@event, Is.Not.Null);
            Assert.That(@event.SourceId, Is.EqualTo(commandId));
        }
        #endregion [ Then ]
    }
}
