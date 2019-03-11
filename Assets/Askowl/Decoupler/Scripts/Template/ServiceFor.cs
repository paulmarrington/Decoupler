// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using UnityEditor;
using UnityEngine;
#if TemplateServiceFor || true
using Askowl;
// Add using statements for service library here
#endif

namespace Decoupler.Services {
  /// <a href=""></a><inheritdoc /> //#TBD#//
  [CreateAssetMenu(menuName = "Decoupled/Template/ServiceForXXX", fileName = "TemplateServiceForXXX")]
  public abstract class TemplateServiceForXXX : TemplateServiceAdapter {
    [InitializeOnLoadMethod] private static void DetectService() {
      bool usable = DefineSymbols.HasPackage("XXX") || DefineSymbols.HasFolder("XXX");
      DefineSymbols.AddOrRemoveDefines(addDefines: usable, named: "TemplateServiceForXXX");
    }

    #region Service Entry Points
    #if !TemplateServiceForXXX
    protected override void Prepare() => base.Prepare();

    protected override void LogOnResponse(Emitter emitter) => base.LogOnResponse(emitter);

    /*-EntryPoint...-*/
    /// <inheritdoc />
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
    #if TemplateServiceForXXX
    /*-EntryPoint...-*/
    /// <inheritdoc />
    public override Emitter Call(Service<EntryPointDto> service) => throw new NotImplementedException("EntryPointDto");
    /*-...EntryPoint-*/
    #endif
    #endregion

    // One service override per service method
    // Access the external service here. Save and call dti.Emitter.Fire when service call completes
    // or set dto.ErrorMessage if the service call fails to initialise

    #region Compiler Definition
    #if TemplateServiceForXXX
    public override bool IsExternalServiceAvailable() => true;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void RegisterService() { }
    #else
    public override bool IsExternalServiceAvailable() => false;
    #endif
    #endregion
  }
}