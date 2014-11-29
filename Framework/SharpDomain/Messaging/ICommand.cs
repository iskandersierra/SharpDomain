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

    public interface ICommand<TCommandId> : ICommand
    {
        TCommandId Id { get; }
    }
}
