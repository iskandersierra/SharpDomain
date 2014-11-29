@coredomain
@systemaccessing
Feature: UserFactory
	User factory allows the creation of new User domain objects

Scenario Outline: Creation of a new correct user
	Given A user factory
	When Create user is called over the factory with "<username>" And "<password>" And "<email>"
	Then the resulting user is not null 
	And the resulting user is not yet persisted
	And the result should be a user with "<username>" And "<password>" And "<email>"
	Examples:
	| username | password | email                   |
	| user1    | keycode1 | user1@organization.info |