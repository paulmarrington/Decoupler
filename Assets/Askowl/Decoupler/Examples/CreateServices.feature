Feature: Automated creation of decoupled services

  @CreateEmptyService
  Scenario: Create an empty service
    Given we prepare for a new service
    And we set "New Service Name" to "EmptyService"
    And we create the new service
    When processing is complete
    Then there are no errors in the log
