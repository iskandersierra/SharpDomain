using SharpDomain.Aggregates;
using SharpDomain.Business;
using SharpDomain.CoreDomains.ProjectManagement.Commands;
using SharpDomain.CoreDomains.ProjectManagement.Events;
using SharpDomain.Messaging;
using SharpDomain.Processing;

namespace SharpDomain.CoreDomains.ProjectManagement.CommandHandlers
{

    /// <summary>
    /// Responsibility of this command handler must exclusively be convert a command into the emmission
    /// of events, at the same time it serve as a joint-point in the system to other componentts to cut
    /// and introduce their resposibility like ligging, validation, etc.
    /// </summary>
    public class CreateProjectHandler : ICommandHandler<CreateProject>
    {
        private readonly IAggregateRepositoryFactory _repositoryFactory;
        private readonly IAggregateFactory _aggregateFactory;
        private readonly INewGuidGenerator _guidGenerator;

        public CreateProjectHandler(IAggregateRepositoryFactory repositoryFactory, IAggregateFactory aggregateFactory, INewGuidGenerator guidGenerator)
        {
            _repositoryFactory = repositoryFactory;
            _aggregateFactory = aggregateFactory;
            _guidGenerator = guidGenerator;
        }

        public void HandleCommand(CreateProject command)
        {
            // Some steps could be marked as ASPECT if they can be factored out of the handler
            // Steps
            // - [ASPECT] Log command handling start. Event sourced to another UX service?
            // - [ASPECT] Monitor performance, count hits
            // - [ASPECT] Validate command
            // - Create new aggregate from factory
            // - [ASPECT] Validate security before command application
            // - Apply events to aggregate
            // - [ASPECT] Validate aggregate invariants
            // - [ASPECT] Validate security after command application
            // - Save aggregate to repository
            // - Respond with aggregate id

            using (var repository = _repositoryFactory.CreateRepository())
            {
                var project = _aggregateFactory.Create<Aggregate>();

                project.ApplyEvent(new ProjectCreatedEvent(command.ProjectId)
                {
                    Name = command.Name,
                });
                project.ApplyEvent(new ProjectDescriptionUpdatedEvent
                {
                    SourceId = command.ProjectId,
                    Title = command.Title,
                    Description = command.Description
                });

                var commitId = _guidGenerator.NewId();
                repository.Save(project, commitId);
            }
        }
    }
}
