using SharpDomain.Business;
using SharpDomain.CoreDomains.ProjectManagement.Events;

namespace SharpDomain.CoreDomains.ProjectManagement.CommandHandlers
{
    public class ProjectActivationAggregate : StatefulAggregate
    {
        public ProjectActivationAggregate()
        {
            IsActive = false;
        }

        public bool IsActive { get; private set; }

        private void Apply(ProjectCreated @event)
        {
            IsActive = false;
        }

        private void Apply(ProjectActivated @event)
        {
            IsActive = true;
        }

        private void Apply(ProjectDeactivated @event)
        {
            IsActive = false;
        }
    }
}