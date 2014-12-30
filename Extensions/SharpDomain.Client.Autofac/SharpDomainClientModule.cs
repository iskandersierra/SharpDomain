using System.Configuration;
using System.Xml;
using Autofac;
using SharpDomain.Autofac;
using SharpDomain.Commanding;

namespace SharpDomain.Client.Autofac
{
    public class SharpDomainClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register client command loop if configured to do so
            var registerClientLoop = ConfigurationManager.AppSettings["RegisterClientLoop"];
            if (registerClientLoop != null && XmlConvert.ToBoolean(registerClientLoop))
            {
                builder.RegisterType<CommandSendingLoop>().SingleInstance();
                builder.RegisterAllImplementationsOf<ICommandSendingProvider>().SingleInstance();
            }

            // Register client command bus decorator
            builder.Register(c => new ClientCommandBus(c.ResolveKeyed<ICommandBus>(RegistrationType.Implementation)))
                .Keyed<ICommandBus>(RegistrationType.Client)
                .SingleInstance();
            builder.Register(c => c.ResolveKeyed<ICommandBus>(RegistrationType.Client))
                .As<ICommandBus>()
                .SingleInstance();

            base.Load(builder);
        }
    }
}
