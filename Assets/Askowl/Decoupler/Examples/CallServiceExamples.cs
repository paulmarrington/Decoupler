// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages
#if AskowlTests
using System.Collections;
using Askowl;
using Askowl.Gherkin;
using CustomAsset;
using CustomAsset.Mutable;
using Decoupler.Services;
using UnityEngine;
using UnityEngine.TestTools;
// ReSharper disable MissingXmlDoc

namespace Decoupler.Examples {
  public class CallServiceExamples : PlayModeTests {
    /// Sample service call
    private ServiceExampleServiceAdapter.Add CallService() =>
      ServiceExampleServiceAdapter.Add.Call((firstValue, secondValue));

    // ************ Everything below here is BDD and Test scaffolding ************

    private String mockState;
    private int    firstValue, secondValue;
    private string serviceManagerName;
    // ReSharper disable once NotAccessedField.Local
    private int usageBalance = 1;

    private IEnumerator ServiceTest(string label) {
      yield return Feature.Go(definitionAsset: "DecouplerDefinitions", featureFile: "CallServices", label)
                          .AsCoroutine();
    }

    [UnityTest] public IEnumerator TopDownSuccess()   { yield return ServiceTest("@TopDownSuccess"); }
    [UnityTest] public IEnumerator TopDownFailure()   { yield return ServiceTest("@TopDownFailure"); }
    [UnityTest] public IEnumerator TopDownFallback()  { yield return ServiceTest("@TopDownFallback"); }
    [UnityTest] public IEnumerator RoundRobin()       { yield return ServiceTest("@RoundRobin"); }
    [UnityTest] public IEnumerator Random()           { yield return ServiceTest("@Random"); }
    [UnityTest] public IEnumerator RandomExhaustive() { yield return ServiceTest("@RandomExhaustive"); }

    [Step(@"^a (\S+) stack with (\d+) services$")] public void ServerStack(string[] matches) {
      var assetName = $"{matches[0]}ServicesManager";
      Managers.Add<ServiceExampleServicesManager>(assetName);
      Managers.Add("ServiceExampleServicesManager", Managers.Find(assetName));
    }

    [Step(@"^server success of ""(.*)""$")] public void MockStateOf(string[] matches) {
      mockState      = AssetDb.Load<String>("MockState.asset");
      mockState.Text = matches[0];
    }

    [Step(@"^an add service on the math server$")] public void MathServer() { }

    [Step(@"^we add (\d+) and (\d+)$")] public void AddService(string[] matches) {
      firstValue  = int.Parse(matches[0]);
      secondValue = int.Parse(matches[1]);
    }

    [Step(@"^we will get a result of (\d+)$")] public Emitter AddResult(string[] matches) {
      int                              result = int.Parse(matches[0]);
      ServiceExampleServiceAdapter.Add add    = null;
      return Fiber.Start().WaitFor(_ => (add = CallService()).OnComplete)
                  .Assert(_ => add.response == result, _ => $"Expecting {result}, response {add.response}")
                  .OnComplete;
    }

    [Step(@"^we get a service error$")] public Emitter ServiceError() {
      ServiceExampleServiceAdapter.Add add = null;
      return Fiber.Start().WaitFor(_ => (add = CallService()).OnComplete).Assert(_ => add.Failed).OnComplete;
    }

    [Step(@"^we use service (\d+)$")] public Emitter WeUseService(string[] matches) {
      int                              result = int.Parse(matches[0]);
      ServiceExampleServiceAdapter.Add add    = null;
      firstValue = secondValue                = 0;
      return Fiber.Start().WaitFor(_ => (add = CallService()).OnComplete).WaitFor((add = CallService()).OnComplete)
                  .Assert(_ => add.response == result, $"Expecting {result}, returned service {add.response}")
                  .OnComplete;
    }

    [Step(@"^we repeat the service call$")] public void RepeatServiceCall() { }

    [Step(@"^we will eventually get the same service number twice in a row$")]
    public Emitter TwiceInARow() {
      ServiceExampleServiceAdapter.Add add      = null;
      string                           services = "";
      firstValue = secondValue                  = 0;
      return Fiber.Start().Begin.Do(fiber => services = "")
                  .WaitFor(_ => (add = CallService()).OnComplete)
                  .Do(_ => services += (char) ('0' + add.response))
                  .Repeat(3)
                  .Until(fiber => "0000 1111".Contains(services)).OnComplete;
    }

    [Step(@"^we will never get the same service number twice in a row$")]
    public Emitter NeverTwiceInARow() {
      ServiceExampleServiceAdapter.Add add      = null;
      string                           services = "";
      firstValue = secondValue                  = 0;
      return Fiber.Start().ExitOnError.Begin.Do(_ => services = "").Begin
                  .WaitFor(_ => (add = CallService()).OnComplete)
                  .Do(_ => services += (char) ('0' + add.response))
                  .Repeat(3)
                  .Assert(_ => !"0000 1111".Contains(services), _ => $"Result='{services}', not 0000 or 1111")
                  .Repeat(10).OnComplete;
    }

    [Step(@"^each service is called (\d+) times in a row$")] public void InARow(string[] matches) =>
      usageBalance = int.Parse(matches[0]);

    [Step(@"^we get the same service twice cycling$")] public Emitter RepeatCycling() {
      ServiceExampleServiceAdapter.Add add      = null;
      string                           services = "";
      firstValue = secondValue                  = 0;
      return Fiber.Start().Begin
                  .WaitFor(_ => (add = CallService()).OnComplete)
                  .Do(_ => services += ((char) ('0' + add.response)))
                  .Repeat(4).Assert(_ => "00110".Contains(services), _ => $"Result={services}").OnComplete;
    }
  }
}
#endif