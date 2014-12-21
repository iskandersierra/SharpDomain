@infrastructure
@business
Feature: ReflectionAggregateFactory
	Reflection aggregate factory allow the creation of new clean aggregate instances with default 
	values on its properties

Scenario: A reflection-based aggregate factory returns a new clean aggregate instance
	Given a new reflection-based aggregate factory instance
	When a new aggregate is obtained from the aggregate factory
	Then the aggregate is not null
	And the aggregate type is Aggregate class
	And the aggregate has no uncommitted events
