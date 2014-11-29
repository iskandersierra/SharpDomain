@infrastructure
@messaging
Feature: ExtensionsToICommandBus
	Specification for ExtensionsToICommandBus class
	This class allow to publish commands directly to a ICommandBus without creating envelopes
	on the client code

Scenario: Send a single command to the bus
	Given An command bus
	And A generic command
	When I send the command through the command bus
	Then The bus really send an envelope with the command as a body

Scenario: Send many commands to the bus
	Given An command bus
	And A sequence of generic commands
	When I send the sequence of commands through the command bus in one call to send
	Then The bus really send an envelope for each sent command
