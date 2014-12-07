using System;
using SharpDomain.Messaging;

namespace SharpDomain.CoreDomains.ProjectManagement.Events
{
    public interface ProjectDescriptionUpdated : IEvent
    {
        string Title { get; }
        string Description { get; }
    }

    public class ProjectDescriptionUpdatedEvent : Event, ProjectDescriptionUpdated
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}