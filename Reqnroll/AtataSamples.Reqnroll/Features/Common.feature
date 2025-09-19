@ReusesFeatureDriver
Feature: Common

Scenario: Navigation to Calculations page
	Given I am on Home Page
	When I navigate to Calculations page by header's button
	And I type 20 and 20 to the form
	Then I should see 40 in result field

Scenario: Navigation to Plans page
	Given I am on Home Page
	When I navigate to Plans page by header's button
	Then I verify Plans page

Scenario: Navigation to Plans after Calculations page
	Given I am on Home Page
	When I navigate to Calculations page by header's button
	And I type 20 and 20 to the form
	Then I should see 40 in result field
	When I navigate to Plans page by header's button
	Then I verify Plans page