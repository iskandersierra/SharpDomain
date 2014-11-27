using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.Messaging
{
    public interface ICommand
    {
        object Id { get; }
    }

    public interface ICommand<TCommandId> : ICommand
    {
        new TCommandId Id { get; }
    }
}
