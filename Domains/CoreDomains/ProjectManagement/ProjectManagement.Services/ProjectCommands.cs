
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.CoreDomains.ProjectManagement.Services
{
    public interface ProjectCommands
    {
        void CreateProject(string name, string title, string description);
        void ActivateProject(Guid projectId);
        void DeactivateProject(Guid projectId);
        void UpdateProjectDescription(Guid projectId, string title, string description);
    }
}
