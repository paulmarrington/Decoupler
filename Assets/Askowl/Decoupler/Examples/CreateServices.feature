Feature: Automated creation of decoupled services

  @CreateEmptyService
  Scenario: Create an empty service
    Given we prepare for a new service
    And we set "New Service Name" to unique "EmptyService"
    And we create the new service
#    Due to script reload after script builds, the following cannot be automated at this time
#    When Tell: Wait for processing to complete (you will see an "All Done..." message)
#    Then Ask: Is the log clean, assets filled and source fit for purpose?

  @CreateServiceWithContext
  Scenario: Create an empty service with context items context item
    Given we prepare for a new service
    And we set "New Service Name" to unique "ContextService"
    And we set "Context" to "int i, string s"
    And we create the new service
#    Due to script reload after script builds, the following cannot be automated at this time
#    When Tell: Wait for processing to complete (you will see an "All Done..." message)
#    Then Ask: Is the log clean, assets filled and source fit for purpose?


  @CreateServiceWithEntryPoints
  Scenario: Create a service with entry points
    Given we prepare for a new service
    And we set "New Service Name" to unique "EntryPointService"
    And we add entry points:
      | name   | request         | response                 |
      | Empty  |                 |                          |
      | Simple | int             | string                   |
      | Tuple  | int i, string s | string msg,int errorCode |
    And we create the new service
#    Due to script reload after script builds, the following cannot be automated at this time
#    When Tell: Wait for processing to complete (you will see an "All Done..." message)
#    Then Ask: Is the log clean, assets filled and source fit for purpose?

  @AddConcreteService
  Scenario: Add a concrete service
    And Tell: Select a service we created earlier
    When Tell: Choose Asset // Create // Decoupled // <b>Service Name</b> // Concrete Service
    And Tell: Give the concrete service a name
    And Tell: Press the `Create` button
#    Due to script reload after script builds, the following cannot be automated at this time
#    When Tell: Wait for processing to complete (you will see an "All Done..." message)
#    Then Ask: Is the log clean, assets filled and source fit for purpose?
