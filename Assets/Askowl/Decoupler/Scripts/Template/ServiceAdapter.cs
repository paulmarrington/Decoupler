using System;
using Askowl;
using CustomAsset;
using UnityEngine;
using Object = System.Object;

namespace Decoupler.Services {
  public abstract class
    _Template_ServiceAdapter : Services<_Template_ServiceAdapter, _Template_Context>.ServiceAdapter {
    #region Service Support
    // Code that is common to all services belongs here
    protected override void Prepare() { }
    #endregion

    #region Public Interface
    // Methods calling code will use to call a service - over and above concrete interface methods below ones defined below.
    #endregion

    #region Service Entry Points
    // List of virtual interface methods that all concrete service adapters need to implement.

    /*-EntryPoint...-*/
    /************* START EntryPoint *************/
    public abstract Emitter Call(EntryPoint EntryPoint);

    public class EntryPoint : Entry<int /*-entryPointRequest-*/, int /*-entryPointResponse-*/> {
      static EntryPoint() => Name("_Template_", "EntryPoint");
      public static EntryPoint Call(int /*-entryPointRequest-*/requestDto) => (EntryPoint) Call<EntryPoint>(requestDto);
    }
    /************* END EntryPoint *************/
    /*-...EntryPoint-*/
    #endregion
  }
}