@boundedcontext
@projectmanagement
Feature: ActivateProjectHandler
	This class implements the activation of a project in Project Management Bounded Context

#Scenario Outline: Project activation command must emmit a corresponding project activated event if it was not active already
#	Given a new activate project command handler instance for aggregate with "<projectId>", "<name>", "<title>", "<description>" and "<active>"
#	And a new activate project command with "<projectId>"
#	When the command is handled by the activate project command handler
#	Then a new project aggregate instance is saved with <events> uncommitted events and new version <newVersion>
#	And the event number 1 is a project activated event with "<projectId>"
#	Examples: 
#	| name     | title      | description    | events | newVersion | projectId                              |
#	| project1 | Project #1 | Proj. Desc. #1 | 2      | 2          | {0664E10F-A50A-4540-92EB-64AD8D7837B1} |
#	| project2 | Project #2 |                | 2      | 2          | {0664E10F-A50A-4540-92EB-64AD8D7837B2} |
#	| project2 |            | Proj. Desc. #3 | 2      | 2          | {0664E10F-A50A-4540-92EB-64AD8D7837B3} |
#	|          | Project #2 | Proj. Desc. #4 | 2      | 2          | {0664E10F-A50A-4540-92EB-64AD8D7837B4} |
#	|          |            |                | 2      | 2          | {0664E10F-A50A-4540-92EB-64AD8D7837B5} |

