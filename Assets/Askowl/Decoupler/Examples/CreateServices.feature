Feature: Automated creation of decoupled services

  @CreateEmptyService
  Scenario: Create an empty service
    Given we prepare for a new service
    And we set "New Service Name" to "EmptyService"
    And we create the new service
    When processing is complete
    Then Ask: Is the log clean, assets filled and source fit for purpose?

  @CreateServiceWithContext
  Scenario Outline: Create an empty service with context items context item
    Given we prepare for a new service
    And we set "New Service Name" to "ContextService_<row>"
    And we set "Context" to "<context>"
    And we create the new service
    When Tell: Wait for processing to complete
    Then Ask: Is the log clean, assets filled and source fit for purpose?

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
    When Tell: Wait for processing to complete
    Then Ask: Is the log clean, assets filled and source fit for purpose?

  @AddConcreteService
  Scenario: Add a concrete service
    And Tell: Select a service we created earlier
    When Tell: Choose Asset // Create // Decoupled // <b>Service Name</b> // Concrete Service
    And Tell: Give the concrete service a name
    When Tell: Wait for processing to complete
    Then Ask: Is the log clean, assets filled and source fit for purpose?
