using System.Collections;

namespace SharpDomain.Business
{
    public interface ISaga : IAggregate
    {
        IEnumerable UndispatchedCommands { get; }

        //void ClearUndispatchedCommands();
    }
}