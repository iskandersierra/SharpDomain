using System;

namespace SharpDomain.Reflection
{
    public interface IMultiItemTypeBasedRegistry<TItem> : 
        ITypeBasedRegistry<TItem>
    {
        bool TryGetItems(Type type, out TItem[] items);
    }
}