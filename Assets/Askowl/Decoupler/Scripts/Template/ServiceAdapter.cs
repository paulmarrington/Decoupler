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

    #region Service Interface Methods
    // List of virtual interface methods that all concrete service adapters need to implement.

    // **************** Start of TemplateServiceMethod **************** //
    /// A service dto contains data required to call service and data returned from said call
    public class TemplateServiceDto : DelayedCache<TemplateServiceDto> { }
    /// Abstract services - one per dto type
    public abstract Emitter Call(Service<TemplateServiceDto> service);
    // **************** End of TemplateServiceMethod **************** //
    #endregion
  }
}