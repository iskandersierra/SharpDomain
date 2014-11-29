@infrastructure
@messaging
Feature: Envelope
	Specification for Envelope and Envelope<> class
	An envelope represents a generic messager aware of the messaging mechanism, so besides
	a generic body it can contain other properties like delay times, correlation ids, and others

Scenario Outline: Create an envelope with simple string body using Envelope.Create
	Given An envelope is created with simple string body "<message>" using Envelope.Create
	Then The envelope is not null
	And The envelope type is Envelope of String
	And The envelope body is equals to string "<message>"
	And The envelope is implicitly equals to string "<message>"
Examples:
| message |
| hello   |
|         |
| 1       |

Scenario Outline: Create an envelope with simple string body using new Envelope
	Given An envelope is created with simple string body "<message>" using new Envelope
	Then The envelope is not null
	And The envelope type is Envelope of String
	And The envelope body is equals to string "<message>"
	And The envelope is implicitly equals to string "<message>"
Examples:
| message |
| hello   |
|         |
| 1       |

Scenario Outline: Create an envelope with simple string body using implicit operator
	Given An envelope is created with simple string body "<message>" using implicit operator
	Then The envelope is not null
	And The envelope type is Envelope of String
	And The envelope body is equals to string "<message>"
	And The envelope is implicitly equals to string "<message>"
Examples:
| message |
| hello   |
|         |
| 1       |
