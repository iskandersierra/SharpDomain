using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using SharpDomain.Messaging;
using TechTalk.SpecFlow;

namespace SharpDomain.Specs.Messaging.Steps
{
    [Binding]
    public class ExtensionsToICommandBusSteps
    {
        [Given(@"An command bus")]
        public void GivenAnCommandBus()
        {
            var sentEnvelopes = new List<Envelope<ICommand>>();
            var commandBusMoq = new Mock<ICommandBus>();
            commandBusMoq
                .Setup(bus => bus.Send(It.IsAny<Envelope<ICommand>>()))
                .Callback<Envelope<ICommand>>(sentEnvelopes.Add);
            commandBusMoq
                .Setup(bus => bus.Send(It.IsAny<IEnumerable<Envelope<ICommand>>>()))
                .Callback<IEnumerable<Envelope<ICommand>>>(sentEnvelopes.AddRange);
            var commandBus = commandBusMoq.Object;

            ScenarioContext.Current.Set(commandBusMoq);
            ScenarioContext.Current.Set(commandBus);
            ScenarioContext.Current.Set(sentEnvelopes);
        }

        [Given(@"A generic command")]
        public void GivenAGenericCommand()
        {
            var commandMoq = new Mock<ICommand>();
            var @command = commandMoq.Object;
            ScenarioContext.Current.Set(@command);
        }

        [Given(@"A sequence of generic commands")]
        public void GivenASequenceOfGenericCommands()
        {
            var sequenceOfCommands = new List<ICommand>();
            for (int i = 0; i < 5; i++)
            {
                var commandMoq = new Mock<ICommand>();
                var @command = commandMoq.Object;
                sequenceOfCommands.Add(@command);
            }
            ScenarioContext.Current.Set(sequenceOfCommands);
        }

        [When(@"I send the command through the command bus")]
        public void WhenISendTheCommandThroughTheCommandBus()
        {
            var commandBus = ScenarioContext.Current.Get<ICommandBus>();
            var @command = ScenarioContext.Current.Get<ICommand>();
            commandBus.Send(@command);
        }

        [When(@"I send the sequence of commands through the command bus in one call to send")]
        public void WhenISendTheSequenceOfCommandsThroughTheCommandBusInOneCallToSend()
        {
            var commandBus = ScenarioContext.Current.Get<ICommandBus>();
            var sequenceOfCommands = ScenarioContext.Current.Get<List<ICommand>>();
            commandBus.Send(sequenceOfCommands);
        }

        [Then(@"The bus really send an envelope with the command as a body")]
        public void ThenTheBusReallySendAnEnvelopeWithTheCommandAsABody()
        {
            var @command = ScenarioContext.Current.Get<ICommand>();
            var sentEnvelopes = ScenarioContext.Current.Get<List<Envelope<ICommand>>>();
            
            Assert.That(sentEnvelopes.Count, Is.EqualTo(1));
            Assert.That(sentEnvelopes[0].Body, Is.SameAs(@command));
        }

        [Then(@"The bus really send an envelope for each sent command")]
        public void ThenTheBusReallySendAnEnvelopeForEachSendedCommand()
        {
            var sequenceOfCommands = ScenarioContext.Current.Get<List<ICommand>>();
            var sentEnvelopes = ScenarioContext.Current.Get<List<Envelope<ICommand>>>();

            Assert.That(sentEnvelopes.Count, Is.EqualTo(sequenceOfCommands.Count));
            CollectionAssert.AreEqual(sequenceOfCommands, sentEnvelopes.Select(e => e.Body));
        }
    }
}
