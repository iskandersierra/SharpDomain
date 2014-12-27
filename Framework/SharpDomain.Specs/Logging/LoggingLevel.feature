@logging
@framework
Feature: LoggingLevel
	Enumerated list of logging levels

Scenario: There are exactly seven Logging Levels and they are Trace ... Off
	Given The list of all LogginLevel enum values
	Then The list of all logging levels has 7 values
	And The LoggingLevel at 0 is "Trace" and has ordinal value of 0
	And The LoggingLevel at 1 is "Debug" and has ordinal value of 1
	And The LoggingLevel at 2 is "Info" and has ordinal value of 2
	And The LoggingLevel at 3 is "Warn" and has ordinal value of 3
	And The LoggingLevel at 4 is "Error" and has ordinal value of 4
	And The LoggingLevel at 5 is "Fatal" and has ordinal value of 5
	And The LoggingLevel at 6 is "Off" and has ordinal value of 6
