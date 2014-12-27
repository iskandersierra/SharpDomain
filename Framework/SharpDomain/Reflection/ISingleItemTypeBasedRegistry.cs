using System;

namespace SharpDomain.Reflection
{
    public interface ISingleItemTypeBasedRegistry<TItem> : ITypeBasedRegistry<TItem>
    {
        bool TryGetItem(Type type, out TItem item);
    }
}