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

Scenario Outline: A update contact title command raises a contact title updated event
	Given a update contact title command handler
	And a command handler context
	And a update contact title command is created with "<contactId>" and "<title>"
	When the update contact title command handler handles the command
	Then the command handler context has <eventCount> emmitted events
	And the command handler context has a contact title updated event as event 1 with "<contactId>" and "<title>"
	Examples:
	| contactId                              | title | eventCount |
	| {6010B03D-B110-42CA-87B1-0C6B926C6E4E} | Hello | 1          |

Scenario Outline: A update contact picture command raises a contact picture updated event
	Given a update contact picture command handler
	And a command handler context
	And a update contact picture command is created with "<contactId>" and "<pictureId>"
	When the update contact picture command handler handles the command
	Then the command handler context has <eventCount> emmitted events
	And the command handler context has a contact picture updated event as event 1 with "<contactId>" and "<pictureId>"
	Examples:
	| contactId                              | pictureId                              | eventCount |
	| {6010B03D-B110-42CA-87B1-0C6B926C6E4E} | {1811C884-0030-4F06-8FC0-2E6DCD28FD77} | 1          |

Scenario Outline: A clear contact picture command raises a contact picture cleared event
	Given a clear contact picture command handler
	And a command handler context
	And a clear contact picture command is created with "<contactId>"
	When the clear contact picture command handler handles the command
	Then the command handler context has <eventCount> emmitted events
	And the command handler context has a contact picture cleared event as event 1 with "<contactId>"
	Examples:
	| contactId                              | eventCount |
	| {6010B03D-B110-42CA-87B1-0C6B926C6E4E} | 1          |
