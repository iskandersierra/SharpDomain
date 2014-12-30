using System;
using System.Configuration;
using Autofac;
using NServiceBus;
using SharpDomain.Utilities;

namespace SharpDomain.NServiceBus.Autofac
{
    public abstract class EndpointConfigBase : IConfigureThisEndpoint
    {
        public virtual void Customize(BusConfiguration configuration)
        {
            SharpHelpers.PreLoadDeployedAssemblies();

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(AppDomain.CurrentDomain.GetAssemblies());

            Container = builder.Build();

            configuration.EndpointName(ConfigurationManager.AppSettings["EndpointName"]);
            configuration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(Container));
            configuration.UsePersistence<InMemoryPersistence>();

            var conventions = configuration.Conventions();
            conventions.DefiningCommandsAs(t => t.IsInterface && CommonTypes.DomainCommand.IsAssignableFrom(t));
            conventions.DefiningEventsAs(t => t.IsInterface && CommonTypes.DomainEvent.IsAssignableFrom(t));
            conventions.DefiningMessagesAs(t => t.IsInterface && CommonTypes.DomainMessage.IsAssignableFrom(t));
        }

        protected IContainer Container { get; set; }
    }
}
