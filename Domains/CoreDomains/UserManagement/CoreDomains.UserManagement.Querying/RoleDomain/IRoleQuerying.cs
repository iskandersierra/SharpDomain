using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDomain.Querying;

namespace CoreDomains.UserManagement.Querying.RoleDomain
{
    public interface IRoleQuerying
    {
        IPagedCollection<RoleInfo> GetRoles(GetRolesQuery query);
    }

    public enum RoleActiveState
    {
        Either,
        Active,
        Inactive,
    }

    public interface GetRolesQuery
    {
        IQueryPaging WithPaging { get; set; }

        Guid? WithRoleId { get; set; }
        string WithName { get; set; }
        string WithDescription { get; set; }

        RoleActiveState WithActiveState { get; set; }
    }

    public interface RoleInfo
    {
        Guid RoleId { get; }

        string Name { get; }
        string Description { get; }
        
        bool IsActive { get; }

    }
}
