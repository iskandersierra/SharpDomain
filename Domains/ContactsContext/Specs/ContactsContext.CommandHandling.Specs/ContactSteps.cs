using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactsContext.Commands;
using ContactsContext.Events;
using Moq;
using NUnit.Framework;
using SharpDomain.EventSourcing;
using TechTalk.SpecFlow;

namespace ContactsContext.CommandHandling.Specs
{
    [Binding]
    public class ContactSteps
    {
        private static readonly DefaultMessageCreator MessageCreator = new DefaultMessageCreator();

        [Given(@"a create contact command handler")]
        public void GivenACreateContactCommandHandler()
        {
            var handler = new CreateContactCommandHandler();

            ScenarioContext.Current.Set(handler);
        }

        [Given(@"a command handler context")]
        public void GivenACommandHandlerContext()
        {
            var context = new CommandHandlerContextMock();
            ScenarioContext.Current.Set(context);
        }

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

        [When(@"the create contact command handler handles the command")]
        public void WhenTheCreateContactCommandHandlerHandlesTheCommand()
        {
            var context = ScenarioContext.Current.Get<CommandHandlerContextMock>();
            var command = ScenarioContext.Current.Get<CreateContact>();
            var handler = ScenarioContext.Current.Get<CreateContactCommandHandler>();

            handler.Handle(command, context);
        }

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
            Assert.That(@event.ContactId, Is.EqualTo(commandId));
        }

        [Then(@"the command handler context has a contact title updated event as event (.*) with ""(.*)"" and ""(.*)""")]
        public void ThenTheCommandHandlerContextHasAContactTitleUpdatedEventAsEventWithAnd(int pos, Guid commandId, string title)
        {
            var context = ScenarioContext.Current.Get<CommandHandlerContextMock>();

            var @event = context.EmmittedEvents[pos - 1] as ContactTitleUpdated;
            Assert.That(@event, Is.Not.Null);
            Assert.That(@event.ContactId, Is.EqualTo(commandId));
            Assert.That(@event.Title, Is.EqualTo(title));
        }
    }
}
