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

        #region [ Given command processors ]
        [Given(@"a contact command processor")]
        public void GivenAContactProcessor()
        {
            var processor = new ContactCommandProcessor();

            ScenarioContext.Current.Set(processor);
        }
        #endregion [ Given command processors ]

        #region [ Given command processor context ]
        [Given(@"a command processor context")]
        public void GivenACommandProcessorContext()
        {
            var context = new CommandProcessorContextMock();
            ScenarioContext.Current.Set<ICommandProcessorContext>(context);
        }
        #endregion [ Given command processor context ]

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
        public void GivenAUpdateContactPictureCommandIsCreatedWithAnd(Guid contactId, string picturePath)
        {
            var command = MessageCreator.CreateMessage<UpdateContactPicture>(c =>
            {
                c.ContactId = contactId;
                c.PicturePath = picturePath;
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

        #region [ When processing commands ]
        [When(@"the create contact command processor processes the command")]
        public void WhenTheCreateContactProcessorProcessesTheCommand()
        {
            var context = ScenarioContext.Current.Get<ICommandProcessorContext>();
            var command = ScenarioContext.Current.Get<CreateContact>();
            var processor = ScenarioContext.Current.Get<ContactCommandProcessor>();

            processor.Process(command, context);
        }
        [When(@"the update contact title command processor processes the command")]
        public void WhenTheUpdateContactTitleCommandprocessorprocessesTheCommand()
        {
            var context = ScenarioContext.Current.Get<ICommandProcessorContext>();
            var command = ScenarioContext.Current.Get<UpdateContactTitle>();
            var processor = ScenarioContext.Current.Get<ContactCommandProcessor>();

            processor.Process(command, context);
        }
        [When(@"the update contact picture command processor processes the command")]
        public void WhenTheUpdateContactPictureCommandprocessorprocessesTheCommand()
        {
            var context = ScenarioContext.Current.Get<ICommandProcessorContext>();
            var command = ScenarioContext.Current.Get<UpdateContactPicture>();
            var processor = ScenarioContext.Current.Get<ContactCommandProcessor>();

            processor.Process(command, context);
        }
        [When(@"the clear contact picture command processor processes the command")]
        public void WhenTheClearContactPictureCommandprocessorprocessesTheCommand()
        {
            var context = ScenarioContext.Current.Get<ICommandProcessorContext>();
            var command = ScenarioContext.Current.Get<ClearContactPicture>();
            var processor = ScenarioContext.Current.Get<ContactCommandProcessor>();

            processor.Process(command, context);
        }
        #endregion [ When processing commands ]

        #region [ Then ]
        [Then(@"the command processor context has (.*) emmitted events")]
        public void ThenTheCommandprocessorContextHasEmmittedEvents(int eventCount)
        {
            var context = (CommandProcessorContextMock)ScenarioContext.Current.Get<ICommandProcessorContext>();

            Assert.That(context.EmmittedEvents.Count, Is.EqualTo(eventCount));
        }

        [Then(@"the command processor context has a contact created event as event (.*) with ""(.*)""")]
        public void ThenTheCommandprocessorContextHasAContactCreatedEventAsEventWith(int pos, Guid commandId)
        {
            var context = (CommandProcessorContextMock)ScenarioContext.Current.Get<ICommandProcessorContext>();

            var @event = context.EmmittedEvents[pos - 1] as ContactCreated;
            Assert.That(@event, Is.Not.Null);
        }

        [Then(@"the command processor context has a contact title updated event as event (.*) with ""(.*)"" and ""(.*)""")]
        public void ThenTheCommandprocessorContextHasAContactTitleUpdatedEventAsEventWithAnd(int pos, Guid commandId, string title)
        {
            var context = (CommandProcessorContextMock)ScenarioContext.Current.Get<ICommandProcessorContext>();

            var @event = context.EmmittedEvents[pos - 1] as ContactTitleUpdated;
            Assert.That(@event, Is.Not.Null);
            Assert.That(@event.Title, Is.EqualTo(title));
        }
        [Then(@"the command processor context has a contact picture updated event as event (.*) with ""(.*)"" and ""(.*)""")]
        public void ThenTheCommandprocessorContextHasAContactPictureUpdatedEventAsEventWithAnd(int pos, Guid commandId, string pictureId)
        {
            var context = (CommandProcessorContextMock)ScenarioContext.Current.Get<ICommandProcessorContext>();

            var @event = context.EmmittedEvents[pos - 1] as ContactPictureUpdated;
            Assert.That(@event, Is.Not.Null);
            Assert.That(@event.PicturePath, Is.EqualTo(pictureId));
        }
        [Then(@"the command processor context has a contact picture cleared event as event (.*) with ""(.*)""")]
        public void ThenTheCommandprocessorContextHasAContactPictureClearedEventAsEventWith(int pos, Guid commandId)
        {
            var context = (CommandProcessorContextMock)ScenarioContext.Current.Get<ICommandProcessorContext>();

            var @event = context.EmmittedEvents[pos - 1] as ContactPictureCleared;
            Assert.That(@event, Is.Not.Null);
        }
        #endregion [ Then ]
    }
}
