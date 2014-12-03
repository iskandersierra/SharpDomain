using System;

namespace CoreDomains.UserManagement.RoleDomain.Commands
{
    public interface CreateRole : IRoleCommand
    {
        string Name { get; }

        string Description { get; }
    }
}
