using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharpDomain.Messaging;

namespace SharpDomain.CoreDomains.ProjectManagement.Commands
{
    public interface CreateProject : ICommand
    {
        Guid ProjectId { get; }
        string Name { get; }
        string Title { get; }
        string Description { get; }
    }

    public class CreateProjectCommand : CreateProject
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
