// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using Askowl;

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
    public class EntryPointDto : DelayedCache<EntryPointDto> {
      public int /*-entryPointRequest-*/  request;
      public int /*-entryPointResponse-*/ response;
    }

    public abstract Emitter Call(Service<EntryPointDto> service);
    /*-...EntryPoint-*/
    #endregion
  }
}