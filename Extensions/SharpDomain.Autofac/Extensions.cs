using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;

namespace SharpDomain.Autofac
{
    public static class Extensions
    {
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> 
            RegisterAppDomainTypes(this ContainerBuilder builder, Type openGenericType, Type nongenericBaseType)
        {
            return builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t =>
                {
                    var implementsBase = nongenericBaseType.IsAssignableFrom(t);
                    if (!implementsBase) return false;
                    var obsoleteAttr = t.GetCustomAttribute<ObsoleteAttribute>();
                    if (obsoleteAttr != null) return false;
                    return true;
                })
                .SingleInstance()
                .AsClosedTypesOf(openGenericType);
        }

        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
            RegisterAllImplementationsOf<TService>(this ContainerBuilder builder)
        {
            return RegisterAllImplementationsOf(builder, typeof(TService));
        }

        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
            RegisterAllImplementationsOf(this ContainerBuilder builder, Type serviceType)
        {
            return RegisterAllImplementationsOf(builder, serviceType, AppDomain.CurrentDomain.GetAssemblies());
        }

        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
            RegisterAllImplementationsOf<TService>(this ContainerBuilder builder, IEnumerable<Assembly> assemblies)
        {
            return builder.RegisterAllImplementationsOf(typeof (TService), assemblies);
        }

        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
            RegisterAllImplementationsOf(this ContainerBuilder builder, Type serviceType, IEnumerable<Assembly> assemblies)
        {
            return builder.RegisterAssemblyTypes(assemblies.ToArray())
                .Where(t =>
                {
                    var implementsBase = serviceType.IsAssignableFrom(t);
                    if (!implementsBase) return false;
                    var obsoleteAttr = t.GetCustomAttribute<ObsoleteAttribute>();
                    if (obsoleteAttr != null) return false;
                    return true;
                })
                .As(serviceType);
        }
    }
}
