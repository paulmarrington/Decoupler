// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages
#if AskowlTests

using UnityEngine;

namespace Decoupler.Services {
  /// Services Manager resides in project hierarchy to load and initialise service management
  [CreateAssetMenu(menuName = "Examples/Decouple/ServiceExample/Service", fileName = "ServiceExampleServicesManager")]
  public class ServiceExampleServicesManager : Services<ServiceExampleServiceAdapter, ServiceExampleContext> { }
}
#endif