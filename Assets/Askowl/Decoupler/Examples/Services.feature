Feature: CustomAsset Services

  @TopDownSuccess
  Scenario: Successful Top-Down Service Call
    Given a top-down stack with 2 services
    And server success of "Pass,Fail"
    And an add service on the math server
    When we add 21 and 22
    Then we will get a result of 43

  @TopDownFailure
  Scenario: Top-Down Service Failure
    Given a top-down stack with 2 services
    And server success of "Fail,Fail"
    And an add service on the math server
    When we add 31 and 42
    Then we get a service error
    And a service message of "Service 2 Failed"

  @TopDownFallback
  Scenario: Top-Down Service Fallback Success
    Given a top-down stack with 2 services
    And server success of "Fail,Pass"
    And an add service on the math server
    When we add 11 and 12
    Then we will get a result of 23

  @RoundRobin
  Scenario: Round-Robin Service Stack
    Given a round-robin stack with 2 services
    And each service is called 2 times in a row
    And server success of "Pass,Pass"
    When we repeat the service call
    Then we get the same service twice cycling

  @Random
  Scenario: Random Service Choice
    Given a random stack with 2 services
    And each service is called 2 times in a row
    And server success of "Pass,Pass"
    When we repeat the service call
    Then we will eventually get the same service number twice in a row

  @RandomExhaustive
  Scenario: Exhaustive Random Service Choice
    Given a random-exhaustive stack with 2 services
    And each service is called 2 times in a row
    And server success of "Pass,Pass"
    When we repeat the service call
    Then we will never get the same service number twice in a row
