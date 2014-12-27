@framework
@reflection
Feature: DaoInterfaceCreator

Scenario: Can build a proxy type based on an interface to be proxied
	Given a dao interface creator
	When an interface proxy is built
	Then the built proxy type is not null

Scenario: Can create a message type interface instance without a concrete class
	Given a dao interface creator
	When an interface proxy is built
	And an instance of the proxy type is created
	And all properties of the instance of the proxy are set
	Then all properties of the instance have the expected values
