using System;
using System.Collections.Generic;
using NUnit.Framework;
using SharpDomain.EventSourcing;
using TechTalk.SpecFlow;

namespace ContactsContext.EventSourcing.Specs
{
    [Binding]
    public class CommonSteps
    {

        [Then(@"no exception is raised")]
        public void ThenNoExceptionIsRaised()
        {
            Exception exception;
            Assert.That(ScenarioContext.Current.TryGetValue(out exception), Is.False);
        }

        [Then(@"an argument out of range exception is raised")]
        public void ThenAnArgumentOutOfRangeExceptionIsRaised()
        {
            AnExceptionIsRaised<ArgumentOutOfRangeException>();
        }

        [Then(@"an argument null exception is raised")]
        public void ThenAnArgumentNullExceptionIsRaised()
        {
            AnExceptionIsRaised<ArgumentNullException>();
        }

        [Then(@"an invalid operation exception is raised")]
        public void ThenAnInvalidOperationExceptionIsRaised()
        {
            AnExceptionIsRaised<InvalidOperationException>();
        }

        public static void AnExceptionIsRaised<TException>() where TException : Exception
        {
            Exception exception;
            Assert.That(ScenarioContext.Current.TryGetValue(out exception), Is.True);
            Assert.That(exception, Is.InstanceOf<TException>());
        }

        public static void RunExceptionControlledStep(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                ScenarioContext.Current.Set(exception);
            }
        }
    }

    public class CommandProcessorContextMock : ICommandProcessorContext
    {
        public readonly List<IEvent> EmmittedEvents = new List<IEvent>();
        private readonly IMessageCreator MessageCreator;

        public CommandProcessorContextMock()
        {
            MessageCreator = new DefaultMessageCreator();
        }

        public void Emmit<T>(Action<T> action) where T : class, IEvent
        {
            var message = MessageCreator.CreateMessage<T>();
            action(message);
            EmmittedEvents.Add(message);
        }
    }
}
