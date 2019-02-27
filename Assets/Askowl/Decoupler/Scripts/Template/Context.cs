// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using UnityEngine;

namespace Decoupler.Services {
  /// <a href=""></a> //#TBD#//
  [CreateAssetMenu(menuName = "Decoupled/Template/Context", fileName = "TemplateContext")]
  public class TemplateContext : Services<TemplateServiceAdapter, TemplateContext>.Context {
    /// <a href=""></a> //#TBD#//
    protected bool Equals(TemplateContext other) => base.Equals(other);
  }
}