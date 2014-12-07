using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Moq;
using NUnit.Framework;
using SharpDomain.Aggregates;
using SharpDomain.Business;
using SharpDomain.Messaging;
using TechTalk.SpecFlow;

namespace SharpDomain.CoreDomains.ProjectManagement.Specs
{
    [Binding]
    public class CommonSteps
    {
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



        public static TAggregate GetSavedAggregate<TAggregate>()
            where TAggregate : class, IAggregate
        {
            var repoFactory = (MockAggregateRepositoryFactory)ScenarioContext.Current.Get<IAggregateRepositoryFactory>();
            var repo = repoFactory.CreatedRepositories[0];
            var aggregate = repo.SavedAggregates[0].Aggregate;

            return (TAggregate) aggregate;
        }

        public static TEvent GetCommittedEvent<TEvent>(int eventNumber)
            where TEvent : class, IEvent
        {
            var aggregate = GetSavedAggregate<IAggregate>();
            var @event = (TEvent)aggregate.UncommittedEvents.ElementAt(eventNumber - 1);

            return @event;
        }
    }

    public class MockAggregateRepositoryFactory : IAggregateRepositoryFactory
    {
        private List<MockAggregateRepository> _createdRepositories = new List<MockAggregateRepository>();

        public List<MockAggregateRepository> CreatedRepositories
        {
            get { return _createdRepositories; }
        }

        public IAggregateRepository CreateRepository()
        {
            var repo = new MockAggregateRepository();
            _createdRepositories.Add(repo);
            return repo;
        }
    }

    public class MockAggregateRepository : IAggregateRepository
    {
        private bool _isDisposed;
        private List<AggregateRepositorySave> _savedAggregates = new List<AggregateRepositorySave>();

        public bool IsDisposed
        {
            get { return _isDisposed; }
        }

        public List<AggregateRepositorySave> SavedAggregates
        {
            get { return _savedAggregates; }
        }

        public void Dispose()
        {
            _isDisposed = true;
        }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IAggregate
        {
            throw new NotImplementedException();
        }

        public void Save(IAggregate aggregate, Guid commitId)
        {
            var ev = new AggregateRepositorySave(aggregate, commitId);
            _savedAggregates.Add(ev);
        }
    }

    public class AggregateRepositorySave
    {
        private readonly IAggregate _aggregate;
        private readonly Guid _commitId;

        public IAggregate Aggregate
        {
            get { return _aggregate; }
        }

        public Guid CommitId
        {
            get { return _commitId; }
        }

        public AggregateRepositorySave(IAggregate aggregate, Guid commitId)
        {
            _aggregate = aggregate;
            _commitId = commitId;
        }
    }
}
