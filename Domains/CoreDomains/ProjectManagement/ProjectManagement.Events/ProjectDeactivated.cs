using System;
using SharpDomain.Messaging;

namespace SharpDomain.CoreDomains.ProjectManagement.Events
{
    public interface ProjectDeactivated : IEvent
    {
    }

    public class ProjectDeactivatedEvent : Event, ProjectDeactivated
    {
    }
}