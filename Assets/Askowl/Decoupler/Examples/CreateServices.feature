Feature: Automated creation of decoupled services

  @CreateEmptyService
  Scenario: Create an empty service
    Given we prepare for a new service
    And we set "New Service Name" to "EmptyService"
    And we create the new service
    When processing is complete
    Then manually check that there are no errors in the log

  @CreateServiceWithContext
  Scenario Outline: Create an empty service with context items context item
    Given we prepare for a new service
    And we set "New Service Name" to "ContextService_<row>"
    And we set "Context" to "<context>"
    And we create the new service
    When processing is complete
    Then manually check that there are no errors in the log

    Examples:
      | row | context                       |
      | 1   | int                           |
      | 2   | int i, string s               |
      | 3   | float r, double d, MyObject m |


  @CreateServiceWithEntryPoints
  Scenario: Create a service with entry points
    Given we prepare for a new service
    And we set "New Service Name" to "EntryPointService_<row>"
    And we add entry points:
      | name   | request         | response                 |
      | Empty  |                 |                          |
      | Simple | int             | string                   |
      | Tuple  | int i, string s | string msg,int errorCode |
    And we create the new service
    When processing is complete
    Then manually check that there are no errors in the log

  @AddConcreteService
  Scenario: Add a concrete service
    Given we have to do this manually
    And we have created a service elsewhere
    When we choose "Asset // Create // Decoupled // Service Name // Concrete Service
    And we give the service a name
    Then manually check that there are no errors in the log
