// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

#if _Template_ServiceFor || true
using System;
using Askowl;
using UnityEditor;
using UnityEngine;
// Add using statements for service library here
#endif

namespace Decoupler.Services {
  /*++[CreateAssetMenu(
    menuName = "Decoupled/_Template_/ServiceFor_ConcreteService_", fileName = "_Template_ServiceFor_ConcreteService_")]++*/
  public abstract class _Template_ServiceFor_ConcreteService_ : _Template_ServiceAdapter {
    /*++[InitializeOnLoadMethod]++*/
    private static void DetectService() {
      bool usable = DefineSymbols.HasPackage("_ConcreteService_") || DefineSymbols.HasFolder("_ConcreteService_");
      DefineSymbols.AddOrRemoveDefines(addDefines: usable, named: "_Template_ServiceFor_ConcreteService_");
    }

    #region Service Entry Points
    #if _Template_ServiceFor_ConcreteService_
    protected override void Prepare() => base.Prepare();

    protected override void LogOnResponse(Emitter emitter) => base.LogOnResponse(emitter);

    /*-EntryPoint...-*/
    public override Emitter Call(Service<EntryPointDto> service) => EntryPointDtoFiber.Go(service).OnComplete;

    private class EntryPointDtoFiber : Fiber.Closure<EntryPointDtoFiber, Service<EntryPointDto>> {
      protected override void Activities(Fiber fiber) =>
        // Use Scope.Dto.request: entryPointRequest
        fiber.WaitFor(1 /*external service*/)
             .Do(_ => Scope.Dto.response = default /*fill response*/)
             .Fire(Scope.Emitter);
    } // or use service.Emitter && service.Emitter.Fire() if another fiber is not needed
    /*-...EntryPoint-*/

    #endif
    #endregion

    #region Service Entry Points
    #if !_Template_ServiceFor_ConcreteService_
    /*-EntryPoint...-*/
    public override Emitter Call(Service<EntryPointDto> service) => throw new NotImplementedException("EntryPointDto");
    /*-...EntryPoint-*/
    #endif
    #endregion

    // One service override per service method
    // Access the external service here. Save and call dti.Emitter.Fire when service call completes
    // or set dto.ErrorMessage if the service call fails to initialise

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