using System;

namespace SharpDomain.Commanding
{
    public interface IDomainCommand
    {
        Guid CommandId { get; }
    }
}
