using Autofac;
using SharpDomain.Autofac;

namespace SharpDomain.Client.Autofac
{
    public class SharpDomainClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandSendingLoop>();

            var reg = builder.RegisterAllImplementationsOf<ICommandSendingProvider>().SingleInstance();

            base.Load(builder);
        }
    }
}
