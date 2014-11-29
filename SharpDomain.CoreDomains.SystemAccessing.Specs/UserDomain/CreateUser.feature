Feature: CreateUserCommand
	CreateUser command allows a client to create a new user and obtain its identifier
	The new user must have a unique and well defined username, which corresponds with its email or any other identity
	The username is not initially verified so a workflow must be established to accept the validity of the user
	A password must be provided in order to enter the system

@coredomain
@systemaccessing
Scenario Outline: Create a new correct user
	Given a user creation command with "<username>" and "<password>" and "<commandid>" is created
	And a user commands service to create user with "<userid>" is obtained
	When the user creation command is sent to the user commands service
	Then the result user id must be "<userid>"
	#And the received create user command corresponds with "<username>" and "<password>" and "<commandid>"
	Examples:
	| username        | password | commandid                              | userid                                 |
	| user1           | pwd1     | {475E1251-0AC2-48B4-81FD-2F2B01093219} | {99A26868-237C-42EB-8232-602A2497EA91} |
	| user2@gmail.com | pwd2     | {408D84EE-8BD6-45D7-99C5-0230E0104CFF} | {4F54C8B6-FD2D-4DA1-A9FF-539965E2265D} |