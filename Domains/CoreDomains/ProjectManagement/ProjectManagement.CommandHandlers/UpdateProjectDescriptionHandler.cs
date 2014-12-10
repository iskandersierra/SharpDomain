using SharpDomain.Aggregates;
using SharpDomain.Business;
using SharpDomain.CoreDomains.ProjectManagement.Commands;
using SharpDomain.CoreDomains.ProjectManagement.Events;
using SharpDomain.Messaging;
using SharpDomain.Processing;

namespace SharpDomain.CoreDomains.ProjectManagement.CommandHandlers
{
    public class UpdateProjectDescriptionHandler : ICommandHandler<UpdateProjectDescription>
    {
        private readonly IAggregateRepositoryFactory _repositoryFactory;
        private readonly INewGuidGenerator _guidGenerator;

        public UpdateProjectDescriptionHandler(IAggregateRepositoryFactory repositoryFactory, INewGuidGenerator guidGenerator)
        {
            _repositoryFactory = repositoryFactory;
            _guidGenerator = guidGenerator;
        }

        public void HandleCommand(UpdateProjectDescription command)
        {

            using (var repository = _repositoryFactory.CreateRepository())
            {
                var project = repository.GetById<Aggregate>(command.ProjectId);

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