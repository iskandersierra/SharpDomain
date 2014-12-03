using System;

namespace CoreDomains.UserManagement.RoleDomain.Commands
{
    public interface DeactivateRole
    {
        Guid RoleId { get; }
    }
}