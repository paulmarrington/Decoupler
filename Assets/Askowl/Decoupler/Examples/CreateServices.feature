#noinspection CucumberUndefinedStep
Feature: Automated creation of decoupled services

  @CreateEmptyService
  Scenario: Create an empty service
    Given we prepare for a new service
    And we set "New Service Name" to unique "EmptyService"
    When we create the new service
#    Then Ask: Wait for processing to complete. Is the log clean of error messages?
#    Will only exit automatically if editor option `Recompile After Finished Playing` is set
#    Don't forget to use the `Build Assets` menu item once the scripts are ready

  @CreateServiceWithContext
  Scenario: Create an empty service with context items context item
    Given we prepare for a new service
    And we set "New Service Name" to unique "ContextService"
    And we set "Context" to "int i, string s"
    When we create the new service
#    Then Ask: Wait for processing to complete. Is the log clean of error messages?
#    Will only exit automatically if editor option `Recompile After Finished Playing` is set
#    Don't forget to use the `Build Assets` menu item once the scripts are ready

  @CreateServiceWithEntryPoints
  Scenario: Create a service with entry points
    Given we prepare for a new service
    And we set "New Service Name" to unique "EntryPointService"
    And we add entry points:
      | name   | request         | response                 |
      | Empty  |                 |                          |
      | Simple | int             | string                   |
      | Tuple  | int i, string s | string msg,int errorCode |
    When we create the new service
#    Then Ask: Wait for processing to complete. Is the log clean of error messages?
#    Will only exit automatically if editor option `Recompile After Finished Playing` is set
#    Don't forget to use the `Build Assets` menu item once the scripts are ready

  @AddConcreteService
  Scenario: Add a concrete service
    Given a service we created earlier
    When we open the concrete service wizard
    And give the concrete service a name
    When we create the new concrete service
#    Then Ask: Wait for processing to complete. Is the log clean of error messages?
#    Will only exit automatically if editor option `Recompile After Finished Playing` is set
#    Don't forget to use the `Build Assets` menu item once the script is ready
