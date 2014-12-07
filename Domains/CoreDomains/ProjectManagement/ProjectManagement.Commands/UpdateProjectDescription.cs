using System;
using SharpDomain.Messaging;

namespace SharpDomain.CoreDomains.ProjectManagement.Commands
{
    public interface UpdateProjectDescription : ICommand
    {
        Guid ProjectId { get; }
        string Title { get; }
        string Description { get; }
    }

    public class UpdateProjectDescriptionCommand : UpdateProjectDescription
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}