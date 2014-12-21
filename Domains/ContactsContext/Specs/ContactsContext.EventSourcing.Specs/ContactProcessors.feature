@domain
@contacts
@commandprocessor
@eventsourcing
Feature: ContactProcessors
	Contact command processors specifications

Scenario Outline: A create contact command raises a contact created and a contact title updated events
	Given a contact command processor
	Given a command processor context
	And a create contact command is created with "<contactId>" and "<title>"
	When the create contact command processor processes the command
	Then the command processor context has <eventCount> emmitted events
	And the command processor context has a contact created event as event 1 with "<contactId>"
	And the command processor context has a contact title updated event as event 2 with "<contactId>" and "<title>"
	Examples:
	| contactId                              | title    | eventCount |
	| {6010B03D-B110-42CA-87B1-0C6B926C6E4E} | Iskander | 2          |
	| {4573A8A5-6E33-4BFA-8F5F-4639AAF44A23} |          | 2          |

Scenario Outline: A update contact title command raises a contact title updated event
	Given a contact command processor
	And a command processor context
	And a update contact title command is created with "<contactId>" and "<title>"
	When the update contact title command processor processes the command
	Then the command processor context has <eventCount> emmitted events
	And the command processor context has a contact title updated event as event 1 with "<contactId>" and "<title>"
	Examples:
	| contactId                              | title | eventCount |
	| {6010B03D-B110-42CA-87B1-0C6B926C6E4E} | Hello | 1          |

Scenario Outline: A update contact picture command raises a contact picture updated event
	Given a contact command processor
	And a command processor context
	And a update contact picture command is created with "<contactId>" and "<picturePath>"
	When the update contact picture command processor processes the command
	Then the command processor context has <eventCount> emmitted events
	And the command processor context has a contact picture updated event as event 1 with "<contactId>" and "<picturePath>"
	Examples:
	| contactId                              | picturePath                     | eventCount |
	| {6010B03D-B110-42CA-87B1-0C6B926C6E4E} | http://pics.example.com/pic.png | 1          |

Scenario Outline: A clear contact picture command raises a contact picture cleared event
	Given a contact command processor
	And a command processor context
	And a clear contact picture command is created with "<contactId>"
	When the clear contact picture command processor processes the command
	Then the command processor context has <eventCount> emmitted events
	And the command processor context has a contact picture cleared event as event 1 with "<contactId>"
	Examples:
	| contactId                              | eventCount |
	| {6010B03D-B110-42CA-87B1-0C6B926C6E4E} | 1          |
