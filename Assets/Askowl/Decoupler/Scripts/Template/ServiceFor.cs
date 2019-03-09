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

    #if TemplateServiceForXXX || true
    protected override void Prepare() => base.Prepare();

    protected override void LogOnResponse(Emitter emitter) => base.LogOnResponse(emitter);

    // Implement all interface methods that call concrete service adapters need to implement

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

    #endif
  }
}