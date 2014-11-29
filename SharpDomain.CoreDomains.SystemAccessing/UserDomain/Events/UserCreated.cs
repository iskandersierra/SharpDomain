using System;
using SharpDomain.Messaging;

namespace SharpDomain.CoreDomains.SystemAccessing.UserDomain.Events
{
    public interface UserCreated : IEvent<Guid>
    {
        string UserName { get; }
        string Password { get; }
        string Email { get; }
    }
}
