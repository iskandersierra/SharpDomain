using SharpDomain.Aggregates;
using SharpDomain.Business;
using SharpDomain.CoreDomains.ProjectManagement.Commands;
using SharpDomain.CoreDomains.ProjectManagement.Events;
using SharpDomain.Messaging;
using SharpDomain.Processing;

namespace SharpDomain.CoreDomains.ProjectManagement.CommandHandlers
{
    public class DeactivateProjectHandler : ICommandHandler<DeactivateProject>
    {
        private readonly IAggregateRepositoryFactory _repositoryFactory;
        private readonly INewGuidGenerator _guidGenerator;

        public DeactivateProjectHandler(IAggregateRepositoryFactory repositoryFactory, INewGuidGenerator guidGenerator)
        {
            _repositoryFactory = repositoryFactory;
            _guidGenerator = guidGenerator;
        }

        public void HandleCommand(DeactivateProject command)
        {

            using (var repository = _repositoryFactory.CreateRepository())
            {
                var project = repository.GetById<ProjectActivationAggregate>(command.ProjectId);

                project.ApplyEvent(new ProjectDeactivatedEvent
                {
                    SourceId = command.ProjectId,
                });

                var commitId = _guidGenerator.NewId();
                repository.Save(project, commitId);
            }
        }
    }
}