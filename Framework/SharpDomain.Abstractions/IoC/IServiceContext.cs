using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.IoC
{
    public interface IServiceContext
    {
        TService Resolve<TService>();
        object Resolve(Type serviceType);
        bool TryResolve<TService>(out TService service) where TService : class;
        bool TryResolve(Type serviceType, out object service);
    }

    public interface IServiceScope : IServiceContext, IDisposable
    {
        IServiceScope CreateNestedScope(Action<IServiceScopeUpdater> setupServices);
    }

    public enum ServiceLifetime
    {
        InstancePerScope,
        InstancePerDependency,
        SingleInstance,
    }

    public interface IServiceScopeUpdater
    {
        void RegisterInstance(object instance, Type[] asServices, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true);
        void RegisterInstance<TService>(TService instance, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true) where TService : class;
        void RegisterInstance<TService, TService2>(TService instance, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true) where TService : class;
        void RegisterInstance<TService, TService2, TService3>(TService instance, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true) where TService : class;

        void RegisterType(Type implementorType, Type[] asServices, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true);
        void RegisterType<TService>(ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true);
        void RegisterType<TImplementor, TService>(ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true) where TImplementor : TService;
        void RegisterType<TImplementor, TService, TService2>(ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true) where TImplementor : TService;
        void RegisterType<TImplementor, TService, TService2, TService3>(ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true) where TImplementor : TService;

        void Register(Func<IServiceContext, object> serviceFactory, Type[] asServices, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true);
        void Register<TService>(Func<IServiceContext, TService> serviceFactory, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true);
        void Register<TService, TService2>(Func<IServiceContext, TService> serviceFactory, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true);
        void Register<TService, TService2, TService3>(Func<IServiceContext, TService> serviceFactory, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true);
    }
}
