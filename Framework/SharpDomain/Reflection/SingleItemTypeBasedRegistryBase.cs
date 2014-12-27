using System;
using System.Collections.Generic;

namespace SharpDomain.Reflection
{
    public class SingleItemTypeBasedRegistryBase<TItem> : 
        ISingleItemTypeBasedRegistry<TItem>
    {
        private readonly Dictionary<Type, TItem> _registry = new Dictionary<Type, TItem>();

        public virtual void Register(Type type, TItem item)
        {
            _registry.Add(type, item);
        }

        public virtual bool TryGetItem(Type type, out TItem item)
        {
            return _registry.TryGetValue(type, out item);
        }
    }
}