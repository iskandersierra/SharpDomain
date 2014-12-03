﻿namespace CoreDomains.UserManagement.RoleDomain.Events
{
    public interface RoleCreated : IRoleEvent
    {
        string Name { get; }

        string Description { get; }
    }
}
