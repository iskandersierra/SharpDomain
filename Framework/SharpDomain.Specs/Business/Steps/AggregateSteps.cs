using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SharpDomain.Business;
using SharpDomain.Messaging;
using TechTalk.SpecFlow;

namespace SharpDomain.Specs.Business.Steps
{
    [Binding]
    public class AggregateSteps
    {

        #region [ Aggregate factory setup ]

        [Given(@"a new reflection-based aggregate factory instance")]
        public void GivenANewAggregateFactoryInstance()
        {
            var factory = new ReflectionAggregateFactory();
            ScenarioContext.Current.Set<IAggregateFactory>(factory);
        }

        #endregion

        #region [ Aggregate creation setup ]

        [Given(@"a new aggregate is obtained from the aggregate factory")]
        [When(@"a new aggregate is obtained from the aggregate factory")]
        public void WhenANewAggregateIsObtainedFromTheAggregateFactory()
        {
            var factory = ScenarioContext.Current.Get<IAggregateFactory>();
            var aggregate = factory.Create<Aggregate>();
            ScenarioContext.Current.Set<IAggregate>(aggregate);
        }

        [Given(@"a new stateful aggregate is obtained from the aggregate factory")]
        public void GivenANewStatefulAggregateIsObtainedFromTheAggregateFactory()
        {
            var factory = ScenarioContext.Current.Get<IAggregateFactory>();
            var aggregate = factory.Create<StatefulTestAggregate>();
            ScenarioContext.Current.Set<IAggregate>(aggregate);
        }

        #endregion

        #region [ Aggregate event applying setup ]

        [When(@"a null is applied to the aggregate")]
        public void WhenANullIsAppliedToTheAggregate()
        {
            var aggregate = ScenarioContext.Current.Get<IAggregate>();
            CommonSteps.RunExceptionControlledStep(() => aggregate.ApplyEvent(null));
        }

        [When(@"a new event ""(.*)"" of type TestAggregateCreated is applied to the aggregate with id ""(.*)""")]
        public void WhenANewEventEOfTypeTestAggregateCreatedIsAppliedToTheAggregateWithId(string @eventName, Guid aggregateId)
        {
            var createdEvent = new TestAggregateCreatedEvent(aggregateId);
            ScenarioContext.Current.Set<IEvent>(createdEvent, @eventName);

            var aggregate = ScenarioContext.Current.Get<IAggregate>();
            CommonSteps.RunExceptionControlledStep(() => aggregate.ApplyEvent(createdEvent));
        }

        [When(@"a new event ""(.*)"" of type TestAggregateModified is applied to the aggregate with value ""(.*)""")]
        public void WhenANewEventOfTypeTestAggregateModifiedIsAppliedToTheAggregateWithId(string @eventName, string value)
        {
            var modifiedEvent = new TestAggregateModifiedEvent() { Value = value };
            ScenarioContext.Current.Set<IEvent>(modifiedEvent, @eventName);

            var aggregate = ScenarioContext.Current.Get<IAggregate>();
            CommonSteps.RunExceptionControlledStep(() => aggregate.ApplyEvent(modifiedEvent));
        }

        [When(@"a new event ""(.*)"" of type TestAggregateModified version two is applied to the aggregate with value ""(.*)"" and int value (.*)")]
        public void WhenANewEventOfTypeTestAggregateModifiedVersionTwoIsAppliedToTheAggregateWithValue(string @eventName, string value, int intValue)
        {
            var modifiedEvent = new TestAggregateModifiedEvent_V2() { Value = value, IntValue = intValue };
            ScenarioContext.Current.Set<IEvent>(modifiedEvent, @eventName);

            var aggregate = ScenarioContext.Current.Get<IAggregate>();
            CommonSteps.RunExceptionControlledStep(() => aggregate.ApplyEvent(modifiedEvent));
        }

        [When(@"a new event ""(.*)"" of type TestAggregateOther is applied to the aggregate")]
        public void WhenANewEventOfTypeTestAggregateOtherIsAppliedToTheAggregate(string @eventName)
        {
            var otherEvent = new TestAggregateOtherEvent();
            ScenarioContext.Current.Set<IEvent>(otherEvent, @eventName);

            var aggregate = ScenarioContext.Current.Get<IAggregate>();
            CommonSteps.RunExceptionControlledStep(() => aggregate.ApplyEvent(otherEvent));
        }
        #endregion

        #region [ Event assertions ]

        [Then(@"the version of event ""(.*)"" is (.*)")]
        public void ThenTheVersionOfEventIs(string @eventName, int version)
        {
            var @event = ScenarioContext.Current.Get<IEvent>(@eventName);

            Assert.That(@event.Version, Is.EqualTo(version));
        }

        [Then(@"the source id of event ""(.*)"" is ""(.*)""")]
        public void ThenTheSourceIdOfEventIs(string @eventName, Guid sourceId)
        {
            var @event = ScenarioContext.Current.Get<IEvent>(@eventName);

            Assert.That(@event.SourceId, Is.EqualTo(sourceId));
        }

        #endregion

        #region [ Aggregate assertions ]

        [Then(@"the aggregate is not null")]
        public void ThenTheAggregateIsNotNull()
        {
            var aggregate = ScenarioContext.Current.Get<IAggregate>();

            Assert.That(aggregate, Is.Not.Null);
        }

        [Then(@"the aggregate type is Aggregate class")]
        public void ThenTheAggregateTypeIsAggregateClass()
        {
            var aggregate = ScenarioContext.Current.Get<IAggregate>();

            Assert.That(aggregate, Is.InstanceOf<Aggregate>());
        }

        [Then(@"the aggregate type is StatefulTestAggregate class")]
        public void ThenTheAggregateTypeIsStatefulTestAggregateClass()
        {
            var aggregate = ScenarioContext.Current.Get<IAggregate>();

            Assert.That(aggregate, Is.InstanceOf<StatefulTestAggregate>());
        }

        [Then(@"the aggregate id is an empty Guid")]
        public void ThenTheAggregateIdIsAnEmptyGuid()
        {
            var aggregate = ScenarioContext.Current.Get<IAggregate>();

            Assert.That(aggregate.Id, Is.EqualTo(Guid.Empty));
        }

        [Then(@"the aggregate id is Guid ""(.*)""")]
        public void ThenTheAggregateIdIsGuid(Guid aggregateId)
        {
            var aggregate = ScenarioContext.Current.Get<IAggregate>();

            Assert.That(aggregate.Id, Is.EqualTo(aggregateId));
        }

        [Then(@"the aggregate has (.*) uncommitted events")]
        public void ThenTheAggregateHasUncommittedEvents(int uncommittedEventsCount)
        {
            var aggregate = ScenarioContext.Current.Get<IAggregate>();

            Assert.That(aggregate.UncommittedEvents.Count(), Is.EqualTo(uncommittedEventsCount));
        }

        [Then(@"the aggregate uncommitted event number (.*) is ""(.*)""")]
        public void ThenTheAggregateUncommittedEventNumberIs(int eventNumber, string @eventName)
        {
            var aggregate = ScenarioContext.Current.Get<IAggregate>();
            var @event = ScenarioContext.Current.Get<IEvent>(@eventName);

            Assert.That(aggregate.UncommittedEvents.ElementAt(eventNumber - 1), Is.SameAs(@event));
        }

        [Then(@"the aggregate version is (.*)")]
        public void ThenTheAggregateVersionIs(int version)
        {
            var aggregate = ScenarioContext.Current.Get<IAggregate>();

            Assert.That(aggregate.Version, Is.EqualTo(version));
        }

        [Then(@"the aggregate has no uncommitted events")]
        public void ThenTheAggregateHasNoUncommittedEvents()
        {
            var aggregate = ScenarioContext.Current.Get<IAggregate>();

            Assert.That(aggregate.UncommittedEvents, Is.Not.Null);
            CollectionAssert.IsEmpty(aggregate.UncommittedEvents);
        }

        [Then(@"the aggregate has (.*) explicitly applied events")]
        public void ThenTheAggregateHasExplicitlyAppliedEvents(int count)
        {
            var aggregate = (StatefulTestAggregate)ScenarioContext.Current.Get<IAggregate>();

            Assert.That(aggregate.AppliedEvents.Count, Is.EqualTo(count));
        }

        [Then(@"the aggregate applied event number (.*) is ""(.*)""")]
        public void ThenTheAggregateAppliedEventNumberIs(int eventNumber, string eventName)
        {
            var aggregate = (StatefulTestAggregate)ScenarioContext.Current.Get<IAggregate>();
            var @event = ScenarioContext.Current.Get<IEvent>(eventName);

            Assert.That(aggregate.AppliedEvents[eventNumber - 1], Is.SameAs(@event));
        }
        #endregion [ Aggregate assertions ]

    }



    #region [ Test types ]

    internal class StatefulTestAggregate : StatefulAggregate
    {
        public List<IEvent> AppliedEvents = new List<IEvent>();

        private void Apply(TestAggregateCreated e)
        {
            AppliedEvents.Add(e);
        }
        private void Apply(TestAggregateModified e)
        {
            AppliedEvents.Add(e);
        }
    }

    internal interface TestAggregateCreated : IAggregateCreatedEvent
    {

    }

    internal class TestAggregateCreatedEvent : AggregateCreatedEvent, TestAggregateCreated
    {
        public TestAggregateCreatedEvent(Guid sourceId)
            : base(sourceId)
        {
        }
    }

    internal interface TestAggregateModified : IEvent
    {
        string Value { get; set; }
    }

    internal class TestAggregateModifiedEvent : Event, TestAggregateModified
    {
        public string Value { get; set; }
    }

    internal interface TestAggregateModified_V2 : TestAggregateModified
    {
        int IntValue { get; set; }
    }

    internal class TestAggregateModifiedEvent_V2 : TestAggregateModifiedEvent, TestAggregateModified_V2
    {
        public int IntValue { get; set; }
    }

    internal interface TestAggregateOther : IEvent
    {
    }

    internal class TestAggregateOtherEvent : Event, TestAggregateOther
    {
    }

    #endregion
}
