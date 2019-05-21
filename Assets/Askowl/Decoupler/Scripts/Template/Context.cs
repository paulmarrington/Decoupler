using UnityEngine;

namespace Decoupler.Services {
  /*--[CreateAssetMenu(menuName = "Decoupled/_Template_/Add Context", fileName = "_Template_Context")]--*/
  public class _Template_Context : Services<_Template_ServiceAdapter, _Template_Context>.Context {
    #region Service Validity Fields
    /*-ContextField...-*/
    [SerializeField] public string /*-contextFieldType-*/ contextFieldName;
    /*-...ContextField-*/

    /// <a href="http://bit.ly/2uRJubf">Equality is used to decide if a service is valid in this context</a>
    protected bool Equals(_Template_Context other) =>
      base.Equals(other) /*-ContextEquals- && Equals(ContextEquality, other.ContextEquality)-*/;
    #endregion

    #region Other Context Fields
    #endregion
  }
}