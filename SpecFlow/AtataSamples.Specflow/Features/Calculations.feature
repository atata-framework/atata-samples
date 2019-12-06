Feature: Calculations
	This feature file contains scenarion for Calculations functionality

Scenario Outline: Check calculation logic
	This scenario should check calculation logic

	Given I am on the Caluclations page
	When I type <argument1> and <argument2> to the form
	Then I should see <result> in result field

Examples: 
        | argument1 | argument2 | result |
        | 10        | 20        | 30     |
        | 1         | 1         | 2      |
        | -1        | 1         | 0      |
        | -100      | 50        | -50    | 