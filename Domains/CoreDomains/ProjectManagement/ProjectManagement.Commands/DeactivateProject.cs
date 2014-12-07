using System;
using SharpDomain.Messaging;

namespace SharpDomain.CoreDomains.ProjectManagement.Commands
{
    public interface DeactivateProject : ICommand
    {
        Guid ProjectId { get; }
    }

    public class DeactivateProjectCommand : DeactivateProject
    {
        public Guid ProjectId { get; set; }
    }
}