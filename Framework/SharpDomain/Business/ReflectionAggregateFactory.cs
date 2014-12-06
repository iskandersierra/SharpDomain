using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace SharpDomain.Business
{

    public class ReflectionAggregateFactory : IAggregateFactory
    {
        private static ConcurrentDictionary<Type, Func<IAggregate>> ConstructorCache;

        static ReflectionAggregateFactory()
        {
            ConstructorCache = new ConcurrentDictionary<Type, Func<IAggregate>>();
        }

        public TAggregate Create<TAggregate>() where TAggregate : class, IAggregate
        {
            var constructor = GetConstructor(typeof(TAggregate));
            var aggregate = constructor();
            return (TAggregate) aggregate;
        }

        private static Func<IAggregate> GetConstructor(Type type)
        {
            var result = ConstructorCache.GetOrAdd(type, CreateConstructor);
            return result;
        }

        private static Func<IAggregate> CreateConstructor(Type type)
        {
            var constructorInfo = type.GetConstructor(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                null, Type.EmptyTypes, null);
            var lambda = Expression.Lambda<Func<IAggregate>>(Expression.New(constructorInfo));
            var func = lambda.Compile();
            return func;
        }
    }
}
