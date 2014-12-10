﻿@boundedcontext
@projectmanagement
Feature: CreateProjectHandler
	This class implements the creation of a new project in Project Management Bounded Context

Scenario Outline: Project creation command with correct data must generate a new project and emmit corresponding events
	Given a new create project command handler instance
	And a new create project command with "<projectId>", "<name>", "<title>" and "<description>"
	When the command is handled by the create project command handler
	Then a new project aggregate instance is saved with <events> uncommitted events and new version <newVersion>
	And the event number 1 is a project created event with "<projectId>" and "<name>"
	And the event number 2 is a project description updated event with "<projectId>", "<title>" and "<description>"
	Examples: 
	| name     | title      | description    | events | newVersion | projectId                              |
	| project1 | Project #1 | Proj. Desc. #1 | 2      | 2          | {0664E10F-A50A-4540-92EB-64AD8D7837B1} |
	| project2 | Project #2 |                | 2      | 2          | {0664E10F-A50A-4540-92EB-64AD8D7837B2} |
	| project2 |            | Proj. Desc. #3 | 2      | 2          | {0664E10F-A50A-4540-92EB-64AD8D7837B3} |
	|          | Project #2 | Proj. Desc. #4 | 2      | 2          | {0664E10F-A50A-4540-92EB-64AD8D7837B4} |
	|          |            |                | 2      | 2          | {0664E10F-A50A-4540-92EB-64AD8D7837B5} |

