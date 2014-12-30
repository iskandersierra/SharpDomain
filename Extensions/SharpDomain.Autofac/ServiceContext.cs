using System;
using Autofac;
using Autofac.Builder;
using SharpDomain.IoC;

namespace SharpDomain.Autofac
{
    public class ServiceContext : IServiceContext
    {
        protected IComponentContext InnerContext { get; private set; }

        public ServiceContext(IComponentContext innerContext)
        {
            InnerContext = innerContext;
        }

        public TService Resolve<TService>()
        {
            return InnerContext.Resolve<TService>();
        }

        public object Resolve(Type serviceType)
        {
            return InnerContext.Resolve(serviceType);
        }

        public bool TryResolve<TService>(out TService service) 
            where TService : class
        {
            service = InnerContext.ResolveOptional<TService>();
            return service != null;
        }

        public bool TryResolve(Type serviceType, out object service)
        {
            service = InnerContext.ResolveOptional(serviceType);
            return service != null;
        }
    }

    public class ServiceScope : ServiceContext, IServiceScope
    {
        protected ILifetimeScope InnerScope { get; private set; }

        public ServiceScope(ILifetimeScope lifetimeScope) 
            : base(lifetimeScope)
        {
            InnerScope = (IContainer) lifetimeScope;
        }

        public void Dispose()
        {
            if (InnerScope != null)
            {
                InnerScope.Dispose();
                InnerScope = null;
                GC.SuppressFinalize(this);
            }
        }

        public IServiceScope CreateNestedScope(Action<IServiceScopeUpdater> setupServices)
        {
            var child = InnerScope.BeginLifetimeScope(builder =>
            {
                var updater = new ServiceScopeUpdater(builder);
                setupServices(updater);
            });
            var result = new ServiceScope(child);
            return result;
        }

        class ServiceScopeUpdater : IServiceScopeUpdater
        {
            ContainerBuilder Builder { get; set; }

            public ServiceScopeUpdater(ContainerBuilder builder)
            {
                Builder = builder;
            }

            public void RegisterInstance(object instance, Type[] asServices, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope,
                bool disposeWhenFinish = true)
            {
                if (instance == null) throw new ArgumentNullException("instance");
                if (asServices == null) throw new ArgumentNullException("asServices");
                if (asServices.Length == 0) throw new ArgumentNullException("asServices");
                var reg = Builder.RegisterInstance(instance).As(asServices);
                CompleteRegistration(reg, lifetime, disposeWhenFinish);
            }

            public void RegisterInstance<T>(T instance, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true) 
                where T : class
            {
                if (instance == null) throw new ArgumentNullException("instance");
                var reg = Builder.RegisterInstance(instance).As<T>();
                CompleteRegistration(reg, lifetime, disposeWhenFinish);
            }

            public void RegisterInstance<T, T2>(T instance, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true) 
                where T : class
            {
                if (instance == null) throw new ArgumentNullException("instance");
                var reg = Builder.RegisterInstance(instance).As<T, T2>();
                CompleteRegistration(reg, lifetime, disposeWhenFinish);
            }

            public void RegisterInstance<T, T2, T3>(T instance, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true) 
                where T : class
            {
                if (instance == null) throw new ArgumentNullException("instance");
                var reg = Builder.RegisterInstance(instance).As<T, T2, T3>();
                CompleteRegistration(reg, lifetime, disposeWhenFinish);
            }

            public void RegisterType(Type implementorType, Type[] asServices, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true)
            {
                if (implementorType == null) throw new ArgumentNullException("implementorType");
                if (asServices == null) throw new ArgumentNullException("asServices");
                if (asServices.Length == 0) throw new ArgumentNullException("asServices");
                var reg = Builder.RegisterType(implementorType).As(asServices);
                CompleteRegistration(reg, lifetime, disposeWhenFinish);
            }

            public void RegisterType<TService>(ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true)
            {
                var reg = Builder.RegisterType<TService>();
                CompleteRegistration(reg, lifetime, disposeWhenFinish);
            }

            public void RegisterType<TImplementor, TService>(ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true) 
                where TImplementor : TService
            {
                var reg = Builder.RegisterType<TImplementor>().As<TService>();
                CompleteRegistration(reg, lifetime, disposeWhenFinish);
            }

            public void RegisterType<TImplementor, TService, TService2>(ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true) 
                where TImplementor : TService
            {
                var reg = Builder.RegisterType<TImplementor>().As<TService, TService2>();
                CompleteRegistration(reg, lifetime, disposeWhenFinish);
            }

            public void RegisterType<TImplementor, TService, TService2, TService3>(ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true) 
                where TImplementor : TService
            {
                var reg = Builder.RegisterType<TImplementor>().As<TService, TService2, TService3>();
                CompleteRegistration(reg, lifetime, disposeWhenFinish);
            }

            public void Register(Func<IServiceContext, object> serviceFactory, Type[] asServices, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true)
            {
                if (serviceFactory == null) throw new ArgumentNullException("serviceFactory");
                if (asServices == null) throw new ArgumentNullException("asServices");
                if (asServices.Length == 0) throw new ArgumentNullException("asServices");
                var reg = Builder.Register(context => serviceFactory(new ServiceContext(context))).As(asServices);
                CompleteRegistration(reg, lifetime, disposeWhenFinish);
            }

            public void Register<TService>(Func<IServiceContext, TService> serviceFactory, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true)
            {
                if (serviceFactory == null) throw new ArgumentNullException("serviceFactory");
                var reg = Builder.Register(context => serviceFactory(new ServiceContext(context))).As<TService>();
                CompleteRegistration(reg, lifetime, disposeWhenFinish);
            }

            public void Register<TService, TService2>(Func<IServiceContext, TService> serviceFactory, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true)
            {
                if (serviceFactory == null) throw new ArgumentNullException("serviceFactory");
                var reg = Builder.Register(context => serviceFactory(new ServiceContext(context))).As<TService, TService2>();
                CompleteRegistration(reg, lifetime, disposeWhenFinish);
            }

            public void Register<TService, TService2, TService3>(Func<IServiceContext, TService> serviceFactory, ServiceLifetime lifetime = ServiceLifetime.InstancePerScope, bool disposeWhenFinish = true)
            {
                if (serviceFactory == null) throw new ArgumentNullException("serviceFactory");
                var reg = Builder.Register(context => serviceFactory(new ServiceContext(context))).As<TService, TService2, TService3>();
                CompleteRegistration(reg, lifetime, disposeWhenFinish);
            }

            private void CompleteRegistration<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> reg, ServiceLifetime lifetime, bool disposeWhenFinish)
            {
                switch (lifetime)
                {
                    case ServiceLifetime.InstancePerScope:
                        reg.InstancePerLifetimeScope();
                        break;
                    case ServiceLifetime.InstancePerDependency:
                        reg.InstancePerDependency();
                        break;
                    case ServiceLifetime.SingleInstance:
                        reg.SingleInstance();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("lifetime");
                }
                if (disposeWhenFinish)
                    reg.OwnedByLifetimeScope();
                else
                    reg.ExternallyOwned();
            }
        }
    }
}
