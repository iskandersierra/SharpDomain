using Autofac;
using SharpDomain.Commanding;
using SharpDomain.NServiceBus.Commanding;

namespace SharpDomain.NServiceBus.Autofac
{
    public class NServiceBusAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register nservicebus implementation of sharpdomain abstractions
            builder
                .RegisterType<NSBCommandBus>()
                .Keyed<ICommandBus>(RegistrationType.Implementation)
                .SingleInstance();

            base.Load(builder);
        }
    }
}
