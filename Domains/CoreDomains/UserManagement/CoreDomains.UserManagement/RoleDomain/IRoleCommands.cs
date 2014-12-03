using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreDomains.UserManagement.RoleDomain.Commands;

namespace CoreDomains.UserManagement.RoleDomain
{
    public interface IRoleCommands
    {
        void Create(CreateRole command);

        void ChangeDescription(ChangeRoleDescription command);

        void Deactivate(DeactivateRole command);
        
        void Activate(ActivateRole command);
    }
}
