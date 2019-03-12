// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using UnityEngine;

namespace Decoupler.Services {
  /// Services Manager resides in project hierarchy to load and initialise service management
  [CreateAssetMenu(menuName = "Decoupled/_Template_/Service", fileName = "_Template_ServicesManager")]
  public class _Template_ServicesManager : Services<_Template_ServiceAdapter, _Template_Context> { }
}