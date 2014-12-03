using System;

namespace SharpDomain.Business
{
    /// <summary>
    /// A memento represents an oppaque current state of an aggregate
    /// </summary>
    public interface IMemento
    {
        Guid Id { get; }

        int Version { get; }
    }
}