using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpDomain.Reflection;
using TechTalk.SpecFlow;

namespace SharpDomain.Specs.Reflection
{
    [Binding]
    public class DaoInterfaceCreatorSteps
    {
        [Given(@"a dao interface creator")]
        public void GivenADaoInterfaceCreator()
        {
            ScenarioContext.Current.Set(new DaoInterfaceProxyBuilder());
        }
        
        [When(@"an interface proxy is built")]
        public void WhenAnInterfaceProxyIsBuilt()
        {
            var builder = ScenarioContext.Current.Get<DaoInterfaceProxyBuilder>();
            var proxyType = builder.GetConcreteType(typeof(InterfaceToProxy));
            
            ScenarioContext.Current.Set(proxyType, "ProxyType");
        }

        [When(@"an instance of the proxy type is created")]
        public void WhenAnInstanceOfTheProxyTypeIsCreated()
        {
            var proxyType = ScenarioContext.Current.Get<Type>("ProxyType");
            var instance = Activator.CreateInstance(proxyType);

            ScenarioContext.Current.Set(instance, "ProxyInstance");
        }

        [When(@"all properties of the instance of the proxy are set")]
        public void WhenAllPropertiesOfTheInstanceOfTheProxyAreSet()
        {
            var instance = ScenarioContext.Current.Get<object>("ProxyInstance");
            var typedInstance = (InterfaceToProxy) instance;

            typedInstance.GuidProperty = new Guid("{01020304-0506-0708-090a-0b0c0d0e0f00}");
            typedInstance.Int32Property = 23;
            typedInstance.StringProperty = "Hello world!";
        }

        [Then(@"all properties of the instance have the expected values")]
        public void ThenAllPropertiesOfTheInstanceHaveTheExpectedValues()
        {
            var instance = ScenarioContext.Current.Get<object>("ProxyInstance");
            var typedInstance = (InterfaceToProxy)instance;

            Assert.That(typedInstance.GuidProperty, Is.EqualTo(new Guid("{01020304-0506-0708-090a-0b0c0d0e0f00}")));
            Assert.That(typedInstance.Int32Property, Is.EqualTo(23));
            Assert.That(typedInstance.StringProperty, Is.EqualTo("Hello world!"));
        }
        [Then(@"the built proxy type is not null")]
        public void ThenTheBuiltProxyTypeIsNotNull()
        {
            var proxyType = ScenarioContext.Current.Get<Type>("ProxyType");

            Assert.That(proxyType, Is.Not.Null);
            Assert.That(proxyType.IsClass, Is.True);
            Assert.That(proxyType.IsAbstract, Is.False);
            Assert.That(proxyType.GetInterfaces(), Is.Not.Null);
            CollectionAssert.AreEqual(proxyType.GetInterfaces(), new Type[] { typeof(InterfaceToProxy) });
        }

    }

    public interface InterfaceToProxy
    {
        Guid GuidProperty { get; set; }
        string StringProperty { get; set; }
        int Int32Property { get; set; }
    }
}
