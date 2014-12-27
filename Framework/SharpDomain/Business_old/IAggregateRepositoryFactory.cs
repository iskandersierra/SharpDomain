using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.Business
{
    public interface IAggregateRepositoryFactory
    {
        IAggregateRepository CreateRepository();
    }
}
