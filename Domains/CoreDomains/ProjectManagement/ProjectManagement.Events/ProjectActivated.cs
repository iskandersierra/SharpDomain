using System;
using SharpDomain.Messaging;

namespace SharpDomain.CoreDomains.ProjectManagement.Events
{
    public interface ProjectActivated : IEvent
    {
    }

    public class ProjectActivatedEvent : Event, ProjectActivated
    {
    }
}