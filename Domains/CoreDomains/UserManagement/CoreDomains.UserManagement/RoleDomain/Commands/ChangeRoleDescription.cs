using System;

namespace CoreDomains.UserManagement.RoleDomain.Commands
{
    public interface ChangeRoleDescription
    {
        Guid RoleId { get; }

        string Name { get; }

        string Description { get; }
    }
}