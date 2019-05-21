using System;
using Askowl;
using UnityEditor;
using UnityEngine; // Do not remove
#if _Template_ServiceFor_ConcreteService_
// Add using statements for service library here
#endif

namespace Decoupler.Services {
  /*++[CreateAssetMenu(
    menuName = "Decoupled/_Template_/ServiceFor_ConcreteService_", fileName = "_Template_ServiceFor_ConcreteService_")]++*/
  public class _Template_ServiceFor_ConcreteService_ : _Template_ServiceAdapter {
    [InitializeOnLoadMethod] private static void DetectService() {
      bool usable = DefineSymbols.HasPackage("_ConcreteService_") || DefineSymbols.HasFolder("_ConcreteService_");
      DefineSymbols.AddOrRemoveDefines(addDefines: usable, named: "_Template_ServiceFor_ConcreteService_");
    }

    #region Service Entry Points
    #if _Template_ServiceFor_ConcreteService_
    protected override void OnEnable()  { base.OnEnable(); }
    protected override void Prepare()   { base.Prepare(); }
    protected override void OnDisable() { base.OnDisable(); }

    /*-EntryPoint...-*/
    public override Emitter Call<T>(EntryPoint<T> EntryPoint) {
      // **************** START FILL - EntryPoint Request Implementation **************** //

      // Step 1: Parse EntryPoint.request to fill request
      // Step 2: Make request
      // Step 3: Wait for response from service
      // Step 4: Fill EntryPoint.response
      // Step 5: Fire EntryPoint.Emitter

      // **************** END FILL - of EntryPoint Request Implementation **************** //
      return EntryPoint.Emitter;
    }
    /*-...EntryPoint-*/

    #endif
    #endregion

    #region Service Entry Points
    #if !_Template_ServiceFor_ConcreteService_
    /*-EntryPoint...-*/
    public override Emitter Call(EntryPoint EntryPoint) => throw new NotImplementedException("EntryPointDto");
    /*-...EntryPoint-*/
    #endif
    #endregion

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