using SharpDomain.NServiceBus.Autofac;

namespace SampleClient
{
    using NServiceBus;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : EndpointConfigBase, AsA_Client
    {
        //public void Customize(BusConfiguration configuration)
        //{
        //    SharpHelpers.PreLoadDeployedAssemblies();

        //    var builder = new ContainerBuilder();

        //    builder.RegisterAssemblyModules(AppDomain.CurrentDomain.GetAssemblies());

        //    var container = builder.Build();

        //    configuration.EndpointName(ConfigurationManager.AppSettings["EndpointName"]);
        //    configuration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(container));
        //    configuration.UsePersistence<InMemoryPersistence>();

        //    var conventions = configuration.Conventions();
        //    conventions.DefiningCommandsAs(t => t.IsInterface && CommonTypes.DomainCommand.IsAssignableFrom(t));
        //    conventions.DefiningEventsAs(t => t.IsInterface && CommonTypes.DomainEvent.IsAssignableFrom(t));
        //    conventions.DefiningMessagesAs(t => t.IsInterface && CommonTypes.DomainMessage.IsAssignableFrom(t));

        //    // NServiceBus provides the following durable storage options
        //    // To use RavenDB, install-package NServiceBus.RavenDB and then use configuration.UsePersistence<RavenDBPersistence>();
        //    // To use SQLServer, install-package NServiceBus.NHibernate and then use configuration.UsePersistence<NHibernatePersistence>();
        //    // If you don't need a durable storage you can also use, configuration.UsePersistence<InMemoryPersistence>();
        //    // more details on persistence can be found here: http://docs.particular.net/nservicebus/persistence-in-nservicebus
        //    //Also note that you can mix and match storages to fit you specific needs. 
        //    //http://docs.particular.net/nservicebus/persistence-order
        //}
    }
}
