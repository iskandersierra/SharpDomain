using System;
using System.Threading;

namespace SharpDomain.Reflection
{
    public class ThreadSafeMultiItemTypeBasedRegistryBase<TItem> :
        MultiItemTypeBasedRegistryBase<TItem>
    {
        private readonly ReaderWriterLockSlim _registryLock = new ReaderWriterLockSlim();

        public override void Register(Type type, TItem item)
        {
            _registryLock.EnterWriteLock();
            try
            {
                base.Register(type, item);
            }
            finally
            {
                _registryLock.ExitWriteLock();
            }
        }

        public override bool TryGetItems(Type type, out TItem[] items)
        {
            _registryLock.EnterWriteLock();
            try
            {
                return base.TryGetItems(type, out items);
            }
            finally
            {
                _registryLock.ExitWriteLock();
            }
        }
    }
}