using System;

namespace CoreDomains.UserManagement.RoleDomain.Commands
{
    public interface ActivateRole
    {
        Guid RoleId { get; }
    }
}