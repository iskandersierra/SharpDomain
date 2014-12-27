using System;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SharpDomain.Specs
{
    [Binding]
    public class CommonSteps
    {

        public static T Get<T>()
        {
            return ScenarioContext.Current.Get<T>();
        }
        public static T Get<T>(string key)
        {
            return ScenarioContext.Current.Get<T>(key);
        }
        public static void Set<T>(T value)
        {
            ScenarioContext.Current.Set<T>(value);
        }
        public static void Set<T>(T value, string key)
        {
            ScenarioContext.Current.Set<T>(value, key);
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
}
