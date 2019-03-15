Feature: Automated creation of decoupled services

  Background:
    Given we are in the project directory "Assets/Temp"
    And we select the menu "Assets/Create/Decoupled/New Service"

  @CreateEmptyService
  Scenario: Create an empty service
    Given we set "New Service Name" to "EmptyService"
    And we press the "Create" button
    When processing is complete
    Then there are no errors in the log
