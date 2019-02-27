// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using UnityEngine;

namespace Decoupler.Services {
  /// Services Manager resides in project hierarchy to load and initialise service management
  [CreateAssetMenu(menuName = "Decoupled/Template/Service", fileName = "TemplateServicesManager")]
  public class TemplateServicesManager : Services<TemplateServiceAdapter, TemplateContext> { }
}