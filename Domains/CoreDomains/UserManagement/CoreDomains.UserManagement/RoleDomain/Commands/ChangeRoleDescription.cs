using System;
using SharpDomain.Messaging;

namespace CoreDomains.UserManagement.RoleDomain.Commands
{
    public interface ChangeRoleDescription : IRoleCommand
    {
        string Name { get; }

        string Description { get; }
    }
}