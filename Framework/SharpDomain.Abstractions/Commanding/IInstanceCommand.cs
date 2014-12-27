using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.Commanding
{
    /// <summary>
    /// Represents a command targeted to a specific aggregate. Note creation commands do no target 
    /// specific instances because aggregate in cuestion do not exist yet. Use ICreationCommand instead
    /// for such cases
    /// </summary>
    public interface IInstanceCommand : IDomainCommand
    {
        Guid AggregateId { get; set; }
    }
}
