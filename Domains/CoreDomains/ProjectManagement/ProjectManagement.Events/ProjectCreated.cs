using System;
using SharpDomain.Messaging;

namespace SharpDomain.CoreDomains.ProjectManagement.Events
{
    public interface ProjectCreated : IAggregateCreatedEvent
    {
        string Name { get; }
        string Password { get; }
    }

    public class ProjectCreatedEvent : AggregateCreatedEvent, ProjectCreated
    {
        public ProjectCreatedEvent(Guid sourceId) : base(sourceId)
        {
        }

        public string Name { get; set; }
        public string Password { get; set; }
    }
}
