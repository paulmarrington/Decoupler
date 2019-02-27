// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using Askowl;
using UnityEditor;
// ReSharper disable MissingXmlDoc

namespace CustomAsset.Services {
  /// <a href=""></a><inheritdoc /> //#TBD#//
  public abstract class
    ServiceExampleServiceAdapter : Services<ServiceExampleServiceAdapter, ServiceExampleContext>.ServiceAdapter {
    #region Service Support
    /// <a href=""></a> //#TBD#//
    protected override void Prepare() { }

    protected override void LogOnResponse(Emitter emitter) {
      var service = emitter.Context<Service>();
      var error   = service.ErrorMessage;
      if (error != default) {
        if (!string.IsNullOrEmpty(error)) Error($"Service Error: {error}");
      } else {
        Log("Success", $"service response for '{GetType().Name}");
      }
    }

    // Code that is common to all services belongs here
    #endregion

    #region Public Interface
    // Methods calling code will use to call a service - over and above concrete interface methods below ones defined below.
    #endregion

    #region Service Interface Methods
    // List of virtual interface methods that all concrete service adapters need to implement.

    // **************** Start of ServiceExampleServiceMethod **************** //
    /// A service dto contains data required to call service and data returned from said call
    public class AddDto : DelayedCache<AddDto> {
      public (int firstValue, int secondValue) request;
      public int                               response;
      public override string ToString() =>
        $"({request.firstValue}, {request.secondValue}) => {response}";
    }
    /// Abstract services - one per dto type
    public abstract Emitter Call(Service<AddDto> service);
    // **************** End of ServiceExampleServiceMethod **************** //
    #endregion

    #region Compiler Definition
    #if ServiceExampleServiceFor
    public override bool IsExternalServiceAvailable() => true;
    #else
    public override bool IsExternalServiceAvailable() => false;
    #endif

    [InitializeOnLoadMethod] private static void DetectService() {
      bool usable = DefineSymbols.HasPackage("") || DefineSymbols.HasFolder("");
      DefineSymbols.AddOrRemoveDefines(addDefines: usable, named: "ServiceExampleServiceFor");
    }
    #endregion
  }
}