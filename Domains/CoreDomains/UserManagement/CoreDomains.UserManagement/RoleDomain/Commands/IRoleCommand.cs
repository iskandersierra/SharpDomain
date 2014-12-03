using System;
using SharpDomain.Messaging;

namespace CoreDomains.UserManagement.RoleDomain.Commands
{
    public interface IRoleCommand : ICommand
    {
        Guid RoleId { get; }
    }
}
