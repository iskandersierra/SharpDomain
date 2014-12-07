using System;
using SharpDomain.Messaging;

namespace SharpDomain.CoreDomains.ProjectManagement.Commands
{
    public interface ActivateProject : ICommand
    {
        Guid ProjectId { get; }
    }

    public class ActivateProjectCommand : ActivateProject
    {
        public Guid ProjectId { get; set; }
    }
}