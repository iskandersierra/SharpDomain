using System;
using SharpDomain.Aggregates;
using SharpDomain.CoreDomains.ProjectManagement.Commands;
using SharpDomain.CoreDomains.ProjectManagement.Services;
using SharpDomain.Processing;

namespace SharpDomain.CoreDomains.ProjectManagement.ServicesImplementation
{
    public class ProjectCommandsImplementation : ProjectCommands
    {
        private INewGuidGenerator _idGenerator;
        private ICommandProcessor _commandProcessor;

        public void CreateProject(string name, string title, string description)
        {
            Guid projectId = _idGenerator.NewId();
            var command = new CreateProjectCommand()
            {
                ProjectId = projectId,
                Name = name,
                Title = title,
                Description = description,
            };
            _commandProcessor.Process(command);
        }

        public void ActivateProject(Guid projectId)
        {
            var command = new ActivateProjectCommand()
            {
                ProjectId = projectId,
            };
            _commandProcessor.Process(command);
        }

        public void DeactivateProject(Guid projectId)
        {
            var command = new DeactivateProjectCommand()
            {
                ProjectId = projectId,
            };
            _commandProcessor.Process(command);
        }

        public void UpdateProjectDescription(Guid projectId, string title, string description)
        {
            var command = new ActivateProjectCommand()
            {
                ProjectId = projectId,
            };
            _commandProcessor.Process(command);
        }
    }
}
