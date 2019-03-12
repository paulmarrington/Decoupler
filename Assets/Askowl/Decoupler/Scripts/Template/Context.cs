// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using UnityEngine;

namespace Decoupler.Services {
  /// <a href=""></a> //#TBD#//
  [CreateAssetMenu(menuName = "Decoupled/_Template_/Context", fileName = "_Template_Context")]
  public class _Template_Context : Services<_Template_ServiceAdapter, _Template_Context>.Context {
    #region Service Validity Fields
    /*-ContextField...-*/
    /// <a href=""></a> //#TBD#//
    [SerializeField] public _Template_Context contextFieldName;
    /*-...ContextField-*/

    /// <a href="">Equality is used to decide if a service is valid in this context</a> //#TBD#//
    protected bool Equals(_Template_Context other) =>
      base.Equals(other) /*--ContextEquals-- && Equals(ContextEquality, other.ContextEquality))--*/;
    #endregion

    #region Other Context Fields
    #endregion
  }
}