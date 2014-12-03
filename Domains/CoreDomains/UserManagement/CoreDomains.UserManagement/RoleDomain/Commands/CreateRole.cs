using System;

namespace CoreDomains.UserManagement.RoleDomain.Commands
{
    public interface CreateRole
    {
        Guid RoleId { get; }

        string Name { get; }

        string Description { get; }
    }
}
