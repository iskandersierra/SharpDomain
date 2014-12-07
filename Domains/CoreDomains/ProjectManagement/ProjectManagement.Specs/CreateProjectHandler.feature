@boundedcontext
@projectmanagement
Feature: CreateProjectHandler
	This class implements the creation of a new project int Project Management Bounded Context

#Scenario: When a correct Create Project command is handled a new Project aggregate is produced and some events are stored and emmitted
#	Given an aggregate repository factory instance
#	And a new aggregate factory instance
#	And a new guid generator
#	And a new create project command handler instance
#	And a new create project command instance with id "{0664E10F-A50A-4540-92EB-64AD8D7837B1}" and name "proj" and title "proj title" and desc "proj desc" and admin password "admin" and active "false"
#	When the command is handled by the create project command handler
#	Then a project created event is produced as event number 1 with id "{0664E10F-A50A-4540-92EB-64AD8D7837B1}" and name "proj"
#	And a project description updated event is produced as event number 2 with title "proj title" and desc "proj desc"
#	And just 2 events are produced
#	#And a CreateUser command is sent to UserService	
	
Scenario Outline: Project creation command with correct data must generate a new project and emmit corresponding events
	Given a new create project command handler instance
	And a new create project command with "<projectId>", "<name>", "<title>", "<description>", "<password>" and "<activate>"
	When the command is handled by the create project command handler
	Then a new project aggregate instance is saved with <events> uncommitted events and new version <newVersion>
	And the event number 1 is a project created event with "<projectId>", "<name>" and "<password>"
	And the event number 2 is a project description updated event with "<projectId>", "<title>" and "<description>"
	And the event number 3 is a project activated event with "<projectId>" if "<activate>" was requested
	#And a create project administrator command is issued through the user service
	Examples: 
	| name     | title      | description    | password  | activate | events | newVersion | projectId                              |
	| project1 | Project #1 | Proj. Desc. #1 | adminpwd1 | false    | 2      | 2          | {0664E10F-A50A-4540-92EB-64AD8D7837B1} |
	| project2 | Project #2 | Proj. Desc. #2 | adminpwd2 | true     | 3      | 3          | {A6091689-2853-45CA-84E7-CFDD56B824E1} |