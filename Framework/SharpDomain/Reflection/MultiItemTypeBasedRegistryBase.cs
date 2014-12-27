using System;
using System.Collections.Generic;

namespace SharpDomain.Reflection
{
    public class MultiItemTypeBasedRegistryBase<TItem> :
        IMultiItemTypeBasedRegistry<TItem>
    {
        private readonly Dictionary<Type, List<TItem>> _registry = new Dictionary<Type, List<TItem>>();

        public virtual void Register(Type type, TItem item)
        {
            List<TItem> list;
            if (!_registry.TryGetValue(type, out list))
            {
                _registry.Add(type, list = new List<TItem>());
            }
            list.Add(item);
        }

        public virtual bool TryGetItems(Type type, out TItem[] items)
        {
            List<TItem> list;
            if (_registry.TryGetValue(type, out list))
            {
                items = list.ToArray();
                return items.Length > 0;
            }
            items = null;
            return false;
        }
    }
}