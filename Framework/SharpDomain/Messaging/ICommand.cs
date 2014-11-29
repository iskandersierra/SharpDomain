using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.Messaging
{
    public interface ICommand
    {
    }

    public interface ICorrelatedCommand : ICommand
    {
        object CorrelationId { get; }
    }

    public interface ICorrelatedCommand<TId> : ICorrelatedCommand
    {
        TId CorrelationId { get; }
    }
}
