using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using SharpDomain.Messaging;
using TechTalk.SpecFlow;

namespace SharpDomain.Specs.Messaging.Steps
{
    [Binding]
    public class ExtensionsToIEventBusSteps
    {
        [Given(@"An event bus")]
        public void GivenAnEventBus()
        {
            var publishedEnvelopes = new List<Envelope<IEvent>>();
            var eventBusMoq = new Mock<IEventBus>();
            eventBusMoq
                .Setup(bus => bus.Publish(It.IsAny<Envelope<IEvent>>()))
                .Callback<Envelope<IEvent>>(publishedEnvelopes.Add);
            eventBusMoq
                .Setup(bus => bus.Publish(It.IsAny<IEnumerable<Envelope<IEvent>>>()))
                .Callback<IEnumerable<Envelope<IEvent>>>(publishedEnvelopes.AddRange);
            var eventBus = eventBusMoq.Object;

            ScenarioContext.Current.Set(eventBusMoq);
            ScenarioContext.Current.Set(eventBus);
            ScenarioContext.Current.Set(publishedEnvelopes);
        }

        [Given(@"A generic event")]
        public void GivenAGenericEvent()
        {
            var eventMoq = new Mock<IEvent>();
            var @event = eventMoq.Object;
            ScenarioContext.Current.Set(@event);
        }

        [Given(@"A sequence of generic events")]
        public void GivenASequenceOfGenericEvents()
        {
            var sequenceOfEvents = new List<IEvent>();
            for (int i = 0; i < 5; i++)
            {
                var eventMoq = new Mock<IEvent>();
                var @event = eventMoq.Object;
                sequenceOfEvents.Add(@event);
            }
            ScenarioContext.Current.Set(sequenceOfEvents);
        }

        [When(@"I publish the event through the event bus")]
        public void WhenIPublishTheEventThroughTheEventBus()
        {
            var eventBus = ScenarioContext.Current.Get<IEventBus>();
            var @event = ScenarioContext.Current.Get<IEvent>();
            eventBus.Publish(@event);
        }

        [When(@"I publish the sequence of events through the event bus in one call to publish")]
        public void WhenIPublishTheSequenceOfEventsThroughTheEventBusInOneCallToPublish()
        {
            var eventBus = ScenarioContext.Current.Get<IEventBus>();
            var sequenceOfEvents = ScenarioContext.Current.Get<List<IEvent>>();
            eventBus.Publish(sequenceOfEvents);
        }

        [Then(@"The bus really publish an envelope with the event as a body")]
        public void ThenTheBusReallyPublishAnEnvelopeWithTheEventAsABody()
        {
            var @event = ScenarioContext.Current.Get<IEvent>();
            var publishedEnvelopes = ScenarioContext.Current.Get<List<Envelope<IEvent>>>();
            
            Assert.That(publishedEnvelopes.Count, Is.EqualTo(1));
            Assert.That(publishedEnvelopes[0].Body, Is.SameAs(@event));
        }

        [Then(@"The bus really publish an envelope for each published event")]
        public void ThenTheBusReallyPublishAnEnvelopeForEachPublishedEvent()
        {
            var sequenceOfEvents = ScenarioContext.Current.Get<List<IEvent>>();
            var publishedEnvelopes = ScenarioContext.Current.Get<List<Envelope<IEvent>>>();

            Assert.That(publishedEnvelopes.Count, Is.EqualTo(sequenceOfEvents.Count));
            CollectionAssert.AreEqual(sequenceOfEvents, publishedEnvelopes.Select(e => e.Body));
        }
    }
}
