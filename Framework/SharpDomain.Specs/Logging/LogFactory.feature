@framework
@logging
Feature: LogFactory

Scenario: The default log of a factory is a log with default name
	Given Any log factory
	When The default log is requested to the log factory
	Then The log with name "default" is returned and its factory is the given one


Scenario: The log associated to a class is a log with full class name
	Given Any log factory
	When The a log is requested to the log factory for class System.Environment
	Then The log with name "System.Environment" is returned and its factory is the given one

Scenario: The log associated to an inner class is a log with full class name
	Given Any log factory
	When The a log is requested to the log factory for class System.Environment.SpecialFolder
	Then The log with name "System.Environment.SpecialFolder" is returned and its factory is the given one

Scenario: The log associated to a generic class is a log with full class name without generic arguments
	Given Any log factory
	When The a log is requested to the log factory for class System.Collections.Generic.List of string
	Then The log with name "System.Collections.Generic.List" is returned and its factory is the given one

Scenario: The log associated to a generic class definition is a log with full class name without generic arguments
	Given Any log factory
	When The a log is requested to the log factory for class System.Collections.Generic.List
	Then The log with name "System.Collections.Generic.List" is returned and its factory is the given one

Scenario: The log associated to an inner struct on a generic class is a log with full class name without generic arguments
	Given Any log factory
	When The a log is requested to the log factory for class Enumerator on System.Collections.Generic.List of string
	Then The log with name "System.Collections.Generic.List.Enumerator" is returned and its factory is the given one

Scenario: The log associated to an inner struct on a generic class definition is a log with full class name without generic arguments
	Given Any log factory
	When The a log is requested to the log factory for class Enumerator on System.Collections.Generic.List
	Then The log with name "System.Collections.Generic.List.Enumerator" is returned and its factory is the given one

Scenario: The log associated to a generic interface is a log with full interface name without generic arguments
	Given Any log factory
	When The a log is requested to the log factory for class System.Collections.Generic.IList of string
	Then The log with name "System.Collections.Generic.IList" is returned and its factory is the given one

Scenario: The log associated to an interface is a log with full interface name
	Given Any log factory
	When The a log is requested to the log factory for class System.Collections.IList
	Then The log with name "System.Collections.IList" is returned and its factory is the given one


Scenario: The log associated to an generic delegate is a log with full delegate name without generic arguments
	Given Any log factory
	When The a log is requested to the log factory for class System.Func of string and bool
	Then The log with name "System.Func" is returned and its factory is the given one


Scenario: The log associated to a class using get current class log is a log with full class name
	Given Any log factory
	And A sample class is created to test get curren class log for it
	Then The log with name "SampleClassToGetCurrentClassLog" is returned and its factory is the given one
