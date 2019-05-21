// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages
#if AskowlTests

using Askowl;
using CustomAsset;
using UnityEngine;
// ReSharper disable MissingXmlDoc

namespace Decoupler.Services {
  public abstract class
    ServiceExampleServiceAdapter : Services<ServiceExampleServiceAdapter, ServiceExampleContext>.ServiceAdapter {
    #region Service Support
    protected override void Prepare() { }
    // Code that is common to all services belongs here
    #endregion

    #region Public Interface
    // Methods calling code will use to call a service - over and above concrete interface methods below ones defined below.
    #endregion

    #region Service Entry Points
    // List of virtual interface methods that all concrete service adapters need to implement.

    // **************** Start of ServiceExampleServiceMethod **************** //
    public abstract Emitter Call(Add add);

    public class Add : Entry<(int firstValue, int secondValue), int> {
      public static Add Call((int firstValue, int secondValue) requestDto) => (Add) Call<Add>(requestDto);
    }
    // **************** End of ServiceExampleServiceMethod **************** //
    #endregion
  }
}
#endif