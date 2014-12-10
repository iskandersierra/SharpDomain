using System;
using System.Linq;
using NUnit.Framework;
using SharpDomain.Aggregates;
using SharpDomain.Business;
using SharpDomain.CoreDomains.ProjectManagement.CommandHandlers;
using SharpDomain.CoreDomains.ProjectManagement.Commands;
using SharpDomain.CoreDomains.ProjectManagement.Events;
using SharpDomain.Messaging;
using SharpDomain.Processing;
using TechTalk.SpecFlow;

namespace SharpDomain.CoreDomains.ProjectManagement.Specs
{
    [Binding]
    public class ProjectSteps
    {
        [Given(@"a new create project command handler instance")]
        public void GivenANewCreateProjectCommandHandlerInstance()
        {
            var repositoryFactory = new MockAggregateRepositoryFactory();
            var aggregateFactory = new ReflectionAggregateFactory();
            var idGen = new NewCombGuidGenerator();
            var handler = new CreateProjectHandler(repositoryFactory, aggregateFactory, idGen);

            ScenarioContext.Current.Set<IAggregateRepositoryFactory>(repositoryFactory);
            ScenarioContext.Current.Set<IAggregateFactory>(aggregateFactory);
            ScenarioContext.Current.Set<INewGuidGenerator>(idGen);
            ScenarioContext.Current.Set<ICommandHandler<CreateProject>>(handler);
        }

        [Given(@"a new create project command with ""(.*)"", ""(.*)"", ""(.*)"" and ""(.*)""")]
        public void GivenANewCreateProjectCommandWithAnd(Guid projectId, string name, string title, string description)
        {
            var command = new CreateProjectCommand()
            {
                ProjectId = projectId,
                Name = name,
                Title = title,
                Description = description,
            };
            ScenarioContext.Current.Set<CreateProject>(command);
        }

        [When(@"the command is handled by the create project command handler")]
        public void WhenTheCommandIsHandledByTheCreateProjectCommandHandler()
        {
            var command = ScenarioContext.Current.Get<CreateProject>();
            var handler = ScenarioContext.Current.Get<ICommandHandler<CreateProject>>();

            handler.HandleCommand(command);
        }

        [Then(@"a new project aggregate instance is saved with (.*) uncommitted events and new version (.*)")]
        public void ThenANewProjectAggregateInstanceIsSavedWithUncommittedEventsAndNewVersion(int eventCount, int newVersion)
        {
            var aggregate = CommonSteps.GetSavedAggregate<Aggregate>();

            Assert.That(aggregate.UncommittedEvents.Count(), Is.EqualTo(eventCount));
            Assert.That(aggregate.Version, Is.EqualTo(newVersion));
        }

        [Then(@"the event number (.*) is a project created event with ""(.*)"" and ""(.*)""")]
        public void ThenTheEventNumberIsAProjectCreatedEventWithAnd(int eventNumber, Guid projectId, string name)
        {
            var @event = CommonSteps.GetCommittedEvent<ProjectCreated>(eventNumber);

            Assert.That(@event.SourceId, Is.EqualTo(projectId));
            Assert.That(@event.Name, Is.EqualTo(name));
        }

        [Then(@"the event number (.*) is a project description updated event with ""(.*)"", ""(.*)"" and ""(.*)""")]
        public void ThenTheEventNumberIsAProjectDescriptionUpdatedEventWithAnd(int eventNumber, Guid projectId, string title, string description)
        {
            var @event = CommonSteps.GetCommittedEvent<ProjectDescriptionUpdated>(eventNumber);

            Assert.That(@event.SourceId, Is.EqualTo(projectId));
            Assert.That(@event.Title, Is.EqualTo(title));
            Assert.That(@event.Description, Is.EqualTo(description));
        }

        [Then(@"the event number (.*) is a project activated event with ""(.*)"" if ""(.*)"" was requested")]
        public void ThenTheEventNumberIsAProjectActivatedEventWithIfWasRequested(int eventNumber, Guid projectId, bool activate)
        {
            if (activate)
            {
                var @event = CommonSteps.GetCommittedEvent<ProjectActivated>(eventNumber);

                Assert.That(@event.SourceId, Is.EqualTo(projectId));
            }
        }
    }
}
