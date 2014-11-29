using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpDomain.CoreDomains.SystemAccessing.UserAggregate;
using SharpDomain.CoreDomains.SystemAccessingImplementation.UserAggregate;
using TechTalk.SpecFlow;

namespace SharpDomain.CoreDomains.SystemAccessing.Specs.UserAggregate.Steps
{
    [Binding]
    public class UserFactorySteps
    {
        [Given(@"A user factory")]
        public void GivenAUserFactory()
        {
            UserFactory userFactory = new EventSourcedUserFactory();
            ScenarioContext.Current.Set(userFactory);
        }

        [When(@"Create user is called over the factory with ""(.*)"" And ""(.*)"" And ""(.*)""")]
        public void WhenCreateUserIsCalledOverTheFactoryWithAndAnd(string username, string password, string email)
        {
            var userFactory = ScenarioContext.Current.Get<UserFactory>();
            User user = userFactory.Create(username, password, email);

            ScenarioContext.Current.Set(user);

        }

        [Then(@"the resulting user is not null")]
        public void ThenTheResultingUserIsNotNull()
        {
            var user = ScenarioContext.Current.Get<User>();

            Assert.That(user, Is.Not.Null);
        }

        [Then(@"the resulting user is not yet persisted")]
        public void ThenTheResultingUserIsNotYetPersisted()
        {
            var user = ScenarioContext.Current.Get<User>();

            Assert.That(user.IsNew, Is.True);
        }

        [Then(@"the result should be a user with ""(.*)"" And ""(.*)"" And ""(.*)""")]
        public void ThenTheResultShouldBeAUserWithAndAnd(string username, string password, string email)
        {
            var user = ScenarioContext.Current.Get<User>();

            Assert.That(user.UserName, Is.EqualTo(username));
            Assert.That(user.Password, Is.EqualTo(password));
            Assert.That(user.Email, Is.EqualTo(email));
        }
    }
}
