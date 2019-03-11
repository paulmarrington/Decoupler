// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using Askowl;

namespace Decoupler.Services {
  /// <a href=""></a><inheritdoc /> //#TBD#//
  public abstract class TemplateServiceAdapter : Services<TemplateServiceAdapter, TemplateContext>.ServiceAdapter {
    #region Service Support
    // Code that is common to all services belongs here
    /// <a href=""></a> //#TBD#//
    protected override void Prepare() { }
    #endregion

    #region Public Interface
    // Methods calling code will use to call a service - over and above concrete interface methods below ones defined below.
    #endregion

    #region Service Entry Points
    // List of virtual interface methods that all concrete service adapters need to implement.

    /*-EntryPoint...-*/
    /// <a href=""></a> //#TBD#//
    public class EntryPointDto : DelayedCache<EntryPointDto> {
      /// <a href=""></a> //#TBD#//
      public int /*-entryPointRequest-*/ request;
      /// <a href=""></a> //#TBD#//
      public int /*-entryPointResponse-*/ response;
    }

    /// <a href=""></a> //#TBD#//
    public abstract Emitter Call(Service<EntryPointDto> service);
    /*-...EntryPoint-*/
    #endregion
  }
}