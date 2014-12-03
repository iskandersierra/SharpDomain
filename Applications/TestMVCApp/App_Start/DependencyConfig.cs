using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MassTransit;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using TestMVCApp.App_Start;

[assembly: PreApplicationStartMethod(typeof(DependencyConfig), "Initialize")]

namespace TestMVCApp.App_Start
{
    public class DependencyConfig
    {
        public static void Initialize()
        {
            var container = BuildContainer();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static Container BuildContainer()
        {
            var container = new Container();
            var singleton = Lifestyle.Singleton;

            // Register in process command bus. Change later by a webservice oriented command bus
            ConfigureBusses(container, singleton);

            container.Verify();

            return container;
        }

        private static void ConfigureBusses(Container container, Lifestyle singleton)
        {
            

        }

    }
}