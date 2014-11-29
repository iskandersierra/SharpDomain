using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDomain.CoreDomains.SystemAccessing.UserDomain.Commands;

namespace SharpDomain.CoreDomains.SystemAccessing.UserDomain
{
    public interface UserCommands : ICommandsService
    {
        Guid CreateUser(CreateUser command);

        void ChangePassword(ChangePassword command);
    }
}
