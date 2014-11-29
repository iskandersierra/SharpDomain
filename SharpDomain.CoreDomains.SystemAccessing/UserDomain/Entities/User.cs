using System;
using SharpDomain.Aggregates;

namespace SharpDomain.CoreDomains.SystemAccessing.UserDomain.Entities
{
    public interface User : IAggregate<Guid>
    {
        string UserName { get; }
        string Password { get; }
        string Email { get; }
    }
}
