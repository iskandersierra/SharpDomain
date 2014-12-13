@domain
@contacts
Feature: ContactCommandHandlers
	Contact command handlers specifications

Scenario Outline: A create contact command raises a contact created and a contact title updated events
	Given a create contact command handler
	Given a command handler context
	And a create contact command is created with "<contactId>" and "<title>"
	When the create contact command handler handles the command
	Then the command handler context has <eventCount> emmitted events
	And the command handler context has a contact created event as event 1 with "<contactId>"
	And the command handler context has a contact title updated event as event 2 with "<contactId>" and "<title>"
	Examples:
	| contactId                              | title    | eventCount |
	| {6010B03D-B110-42CA-87B1-0C6B926C6E4E} | Iskander | 2          |
	| {4573A8A5-6E33-4BFA-8F5F-4639AAF44A23} |          | 2          |