using System;
using System.Threading;

namespace SharpDomain.Reflection
{
    public class ThreadSafeSingleItemTypeBasedRegistryBase<TItem> :
        SingleItemTypeBasedRegistryBase<TItem>
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

        public override bool TryGetItem(Type type, out TItem item)
        {
            _registryLock.EnterWriteLock();
            try
            {
                return base.TryGetItem(type, out item);
            }
            finally
            {
                _registryLock.ExitWriteLock();
            }
        }
    }
}