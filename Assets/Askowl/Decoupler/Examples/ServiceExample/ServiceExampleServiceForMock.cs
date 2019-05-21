// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages
#if AskowlTests

using Askowl;
using CustomAsset.Mutable;
using UnityEngine;
// ReSharper disable MissingXmlDoc

namespace Decoupler.Services {
  [CreateAssetMenu(
    menuName = "Examples/Decouple/ServiceExample/ServiceForMock", fileName = "ServiceExampleServiceForMock")]
  public class ServiceExampleServiceForMock : ServiceExampleServiceAdapter {
    [SerializeField] private String mockState    = default;
    [SerializeField] private int    serviceIndex = 0;

    public override Emitter Call(Add add) {
      var states = mockState.Text.Split(',');
      if (states.Length <= serviceIndex) return null;
      switch (states[serviceIndex]) {
        case "Pass":
          if (add.request.firstValue == 0) {
            add.response = serviceIndex;
          } else {
            add.response = add.request.firstValue + add.request.secondValue;
          }
          Fiber.Start().WaitFor(seconds: 0.01f).Fire(add.Emitter);
          return add.Emitter;
        case "Fail":
          add.Error = $"Service {serviceIndex + 1} Failed";
          return null;
        default:
          add.Error = $"Unknown mock state {mockState.Text}";
          return null;
      }
    }
    /// <inheritdoc />
    public override bool IsExternalServiceAvailable() => true;
  }
}
#endif