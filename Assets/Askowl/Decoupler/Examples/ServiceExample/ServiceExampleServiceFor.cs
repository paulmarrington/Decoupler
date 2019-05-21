// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages
#if AskowlTests

using UnityEngine;
#if ServiceExampleServiceFor
// Add using statements for service library here
// ReSharper disable MissingXmlDoc
#endif

namespace Decoupler.Services {
  [CreateAssetMenu(menuName = "Examples/Decouple/ServiceExample/Service", fileName = "ServiceExampleServiceFor")]
  public abstract class ServiceExampleServiceFor : ServiceExampleServiceAdapter {
    #if ServiceExampleServiceFor
    /// <inheritdoc />
    public override Emitter Call(Service<AddDto> service) {
      // **************** START FILL - EntryPoint Request Implementation **************** //

      // Step 1: Parse Scope.Dto.request to fill request
      // Step 2: Make request
      // Step 3: Wait for response from service
      // Step 4: Fill Scope.Dto.response
      // Step 5: Fire Scope.Emitter

      // **************** END FILL - of EntryPoint Request Implementation **************** //
      return service.Emitter;
    }

    #endif

    #region Compiler Definition
    #if _Template_ServiceFor_ConcreteService_
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void RegisterService() { }
    public override bool IsExternalServiceAvailable() => true;
    #else
    public override bool IsExternalServiceAvailable() => false;
    #endif
    #endregion
  }
}
#endif