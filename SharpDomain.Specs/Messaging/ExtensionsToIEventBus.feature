@infrastructure
@messaging
Feature: ExtensionsToIEventBus
	Specification for ExtensionsToIEventBus class
	This class allow to publish events directly to a IEventBus without creating envelopes
	on the client code

Scenario: Send a single event to the bus
	Given An event bus
	And A generic event
	When I publish the event through the event bus
	Then The bus really publish an envelope with the event as a body

Scenario: Send many events to the bus
	Given An event bus
	And A sequence of generic events
	When I publish the sequence of events through the event bus in one call to publish
	Then The bus really publish an envelope for each published event
