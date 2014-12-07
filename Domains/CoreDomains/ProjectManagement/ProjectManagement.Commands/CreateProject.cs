using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDomain.Messaging;

namespace SharpDomain.CoreDomains.ProjectManagement.Commands
{
    public interface CreateProject : ICommand
    {
        Guid ProjectId { get; }

        string Name { get; }

        string Title { get; }

        string Description { get; }

        string AdministratorPassword { get; }
    }
}
