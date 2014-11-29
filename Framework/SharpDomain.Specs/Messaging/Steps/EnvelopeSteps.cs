using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpDomain.Messaging;
using TechTalk.SpecFlow;

namespace SharpDomain.Specs.Messaging.Steps
{
    [Binding]
    public class EnvelopeSteps
    {
        [Given(@"An envelope is created with simple string body ""(.*)"" using Envelope\.Create")]
        public void GivenAnEnvelopeIsCreatedWithSimpleStringBody(string message)
        {
            ScenarioContext.Current.Set<object>(Envelope.Create(message));
        }

        [Given(@"An envelope is created with simple string body ""(.*)"" using new Envelope")]
        public void GivenAnEnvelopeIsCreatedWithSimpleStringBodyUsingNewEnvelope(string message)
        {
            ScenarioContext.Current.Set<object>(new Envelope<string>(message));
        }

        [Given(@"An envelope is created with simple string body ""(.*)"" using implicit operator")]
        public void GivenAnEnvelopeIsCreatedWithSimpleStringBodyUsingImplicitOperator(string message)
        {
            Envelope<string> envelope = message;
            ScenarioContext.Current.Set<object>(envelope);
        }

        [Then(@"The envelope is not null")]
        public void ThenTheEnvelopeIsNotNull()
        {
            var envelope = ScenarioContext.Current.Get<object>();

            Assert.That(envelope, Is.Not.Null);
        }

        [Then(@"The envelope type is Envelope of String")]
        public void ThenTheEnvelopeTypeIsEnvelopeOfString()
        {
            var envelope = ScenarioContext.Current.Get<object>();

            Assert.That(envelope, Is.InstanceOf<Envelope<string>>());
        }

        [Then(@"The envelope body is equals to string ""(.*)""")]
        public void ThenTheEnvelopeBodyIsEqualsTo(string message)
        {
            var envelope = (Envelope<string>)ScenarioContext.Current.Get<object>();

            Assert.That(envelope.Body, Is.EqualTo(message));
        }

        [Then(@"The envelope is implicitly equals to string ""(.*)""")]
        public void ThenTheEnvelopeIsImplicitlyEqualsToString(string message)
        {
            var envelope = (Envelope<string>)ScenarioContext.Current.Get<object>();

            string body = envelope;

            Assert.That(body, Is.EqualTo(message));
        }
    }
}
