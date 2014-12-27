@framework
@business
Feature: Aggregate
	Aggregate represents a concrete aggregate entity which can be directly used by a domain implementation

Scenario: A new aggregate has a create event applied
	Given a new reflection-based aggregate factory instance
	And a new aggregate is obtained from the aggregate factory
	When a new event "e1" of type TestAggregateCreated is applied to the aggregate with id "{77932DE5-D381-49D9-9CDA-1015C17E0769}"
	Then the aggregate is not null
	And the aggregate type is Aggregate class
	And the aggregate has 1 uncommitted events
	And the aggregate uncommitted event number 1 is "e1"

Scenario: An aggregate must raise an exception when a null event is applied
	Given a new reflection-based aggregate factory instance
	And a new aggregate is obtained from the aggregate factory
	When a null is applied to the aggregate
	Then an argument null exception is raised

#Scenario: An aggregate must raise an exception when a creation event is applied with empty id
#	Given a new reflection-based aggregate factory instance
#	And a new aggregate is obtained from the aggregate factory
#	When a new event "e1" of type TestAggregateCreated is applied to the aggregate with id "{00000000-0000-0000-0000-000000000000}"
#	Then an argument out of range exception is raised

#Scenario: An aggregate must raise an exception when a non-creation event is applied as first event
#	Given a new reflection-based aggregate factory instance
#	And a new aggregate is obtained from the aggregate factory
#	When a new event "e1" of type TestAggregateModified is applied to the aggregate with value "hello there"
#	Then an argument out of range exception is raised

Scenario: A new aggregate has a create and modify events applied
	Given a new reflection-based aggregate factory instance
	And a new aggregate is obtained from the aggregate factory
	When a new event "e1" of type TestAggregateCreated is applied to the aggregate with id "{77932DE5-D381-49D9-9CDA-1015C17E0769}"
	And a new event "e2" of type TestAggregateModified is applied to the aggregate with value "hello there"
	Then the aggregate is not null
	And the aggregate type is Aggregate class
	And the aggregate has 2 uncommitted events
	And the aggregate uncommitted event number 1 is "e1"
	And the aggregate uncommitted event number 2 is "e2"

Scenario: A new stateful aggregate has a create, modify and other events applied events applied
	Given a new reflection-based aggregate factory instance
	And a new stateful aggregate is obtained from the aggregate factory
	When a new event "e1" of type TestAggregateCreated is applied to the aggregate with id "{77932DE5-D381-49D9-9CDA-1015C17E0769}"
	And a new event "e2" of type TestAggregateModified is applied to the aggregate with value "hello there"
	And a new event "e3" of type TestAggregateModified version two is applied to the aggregate with value "hello back" and int value 24
	And a new event "e4" of type TestAggregateOther is applied to the aggregate
	Then the aggregate is not null
	And the aggregate type is StatefulTestAggregate class
	And the aggregate has 4 uncommitted events
	And the aggregate uncommitted event number 1 is "e1"
	And the aggregate uncommitted event number 2 is "e2"
	And the aggregate uncommitted event number 3 is "e3"
	And the aggregate uncommitted event number 4 is "e4"
	And the aggregate has 3 explicitly applied events 
	And the aggregate applied event number 1 is "e1"
	And the aggregate applied event number 2 is "e2"
	And the aggregate applied event number 3 is "e3"
