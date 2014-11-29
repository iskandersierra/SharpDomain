using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDomain.Aggregates;

namespace SharpDomain.CoreDomains.SystemAccessing.UserAggregate
{
    public interface User : IAggregate
    {
        string UserName { get; }
        string Password { get; }
        string Email { get; }
    }
}
