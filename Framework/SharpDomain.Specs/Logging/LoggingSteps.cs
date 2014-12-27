using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using SharpDomain.Logging;
using TechTalk.SpecFlow;

namespace SharpDomain.Specs.Logging
{
    [Binding]
    public class LoggingSteps
    {
        #region [ LoggingLevel steps ]

        [Given(@"The list of all LogginLevel enum values")]
        public void GivenTheListOfAllLogginLevelEnumValues()
        {
            var loggingLevels = Enum.GetValues(typeof (LoggingLevel)).Cast<object>().Select(e => (LoggingLevel) e).ToList();
            Set(loggingLevels);
        }

        [Then(@"The list of all logging levels has (.*) values")]
        public void ThenTheListOfAllLoggingLevelsHasValues(int count)
        {
            var list = Get<List<LoggingLevel>>();

            Assert.That(list.Count, Is.EqualTo(count));
        }

        [Then(@"The LoggingLevel at (.*) is ""(.*)"" and has ordinal value of (.*)")]
        public void ThenTheLoggingLevelAtIsAndHasOrdinalValueOf(int index, LoggingLevel level, int ordinal)
        {
            var list = Get<List<LoggingLevel>>();

            Assert.That(list[index], Is.EqualTo(level));
            Assert.That((int) list[index], Is.EqualTo(ordinal));
        }

        #endregion

        #region [ ILogFactory steps ]

        [Given(@"Any log factory")]
        public void GivenAnyLogFactory()
        {
            var factoryMock = new Mock<ILogFactory>(MockBehavior.Strict);
            var factory = factoryMock.Object;
            factoryMock
                .Setup(e => e.GetLog(It.IsAny<string>()))
                .Returns<string>(name =>
                {
                    var logMock = new Mock<ILog>();
                    logMock.SetupGet(e => e.Name).Returns(name);
                    logMock.SetupGet(e => e.Factory).Returns(factory);
                    return logMock.Object;
                });

            Set(factory);
        }

        [When(@"The default log is requested to the log factory")]
        public void WhenTheDefaultLogIsRequestedToTheLogFactory()
        {
            var factory = Get<ILogFactory>();
            var log = factory.GetDefaultLog();
            Set(log);
        }

        [When(@"The a log is requested to the log factory for class System\.Environment")]
        public void WhenTheALogIsRequestedToTheLogFactoryForClassSystem_Environment()
        {
            var factory = Get<ILogFactory>();
            var log = factory.GetLog(typeof(global::System.Environment));
            Set(log);
        }

        [When(@"The a log is requested to the log factory for class System\.Environment\.SpecialFolder")]
        public void WhenTheALogIsRequestedToTheLogFactoryForClassSystem_Environment_SpecialFolder()
        {
            var factory = Get<ILogFactory>();
            var log = factory.GetLog(typeof(global::System.Environment.SpecialFolder));
            Set(log);
        }

        [When(@"The a log is requested to the log factory for class System\.Collections\.Generic\.List")]
        public void WhenTheALogIsRequestedToTheLogFactoryForClassSystem_Collections_Generic_List()
        {
            var factory = Get<ILogFactory>();
            var log = factory.GetLog(typeof(List<>));
            Set(log);
        }

        [When(@"The a log is requested to the log factory for class System\.Collections\.Generic\.List of string")]
        public void WhenTheALogIsRequestedToTheLogFactoryForClassSystem_Collections_Generic_ListOfString()
        {
            var factory = Get<ILogFactory>();
            var log = factory.GetLog(typeof(List<string>));
            Set(log);
        }

        [When(@"The a log is requested to the log factory for class Enumerator on System\.Collections\.Generic\.List")]
        public void WhenTheALogIsRequestedToTheLogFactoryForClassEnumeratorOnSystem_Collections_Generic_List()
        {
            var factory = Get<ILogFactory>();
            var log = factory.GetLog(typeof(List<>.Enumerator));
            Set(log);
        }

        [When(@"The a log is requested to the log factory for class Enumerator on System\.Collections\.Generic\.List of string")]
        public void WhenTheALogIsRequestedToTheLogFactoryForClassEnumeratorOnSystem_Collections_Generic_ListOfString()
        {
            var factory = Get<ILogFactory>();
            var log = factory.GetLog(typeof(List<string>.Enumerator));
            Set(log);
        }

        [When(@"The a log is requested to the log factory for class System\.Collections\.Generic\.IList of string")]
        public void WhenTheALogIsRequestedToTheLogFactoryForClassSystem_Collections_Generic_IListOfString()
        {
            var factory = Get<ILogFactory>();
            var log = factory.GetLog(typeof(IList<string>));
            Set(log);
        }

        [When(@"The a log is requested to the log factory for class System\.Collections\.IList")]
        public void WhenTheALogIsRequestedToTheLogFactoryForClassSystem_Collections_IList()
        {
            var factory = Get<ILogFactory>();
            var log = factory.GetLog(typeof(IList));
            Set(log);
        }

        [When(@"The a log is requested to the log factory for class System\.Func of string and bool")]
        public void WhenTheALogIsRequestedToTheLogFactoryForClassSystem_FuncOfStringAndBool()
        {
            var factory = Get<ILogFactory>();
            var log = factory.GetLog<Func<string, bool>>();
            Set(log);
        }

        [Given(@"A sample class is created to test get curren class log for it")]
        public void GivenASampleClassIsCreatedToTestGetCurrenClassLogForIt()
        {
            var factory = Get<ILogFactory>();
            var sample = new SampleClassToGetCurrentClassLog(factory);
            var log = sample.Log;
            Set(log);
        }

        [Then(@"The log with name ""(.*)"" is returned and its factory is the given one")]
        public void ThenTheLogWithNameIsReturnedAndItsFactoryIsTheGivenOne(string logName)
        {
            var factory = Get<ILogFactory>();
            var log = Get<ILog>();

            Assert.That(log, Is.Not.Null);
            Assert.That(log.Name, Is.EqualTo(logName));
            Assert.That(log.Factory, Is.SameAs(factory));
        }

        #endregion

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

    }
}

class SampleClassToGetCurrentClassLog
{
    public readonly ILog Log;

    public SampleClassToGetCurrentClassLog(ILogFactory factory)
    {
        Log = factory.GetCurrentClassLog();
    }
}
