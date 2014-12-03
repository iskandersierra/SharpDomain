using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDomains.UserManagement.RoleDomain.Events
{
    public interface RoleCreated
    {
        Guid RoleId { get; }

        string Name { get; }

        string Description { get; }
    }

    public interface RoleDescriptionChanged
    {
        Guid RoleId { get; }

        string Description { get; }
    }

    public interface RoleActivated
    {
        Guid RoleId { get; }
    }

    public interface RoleDeactivated
    {
        Guid RoleId { get; }
    }

}
