// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages
#if AskowlTests

using UnityEngine;

namespace Decoupler.Services {
  /// <a href=""></a> //#TBD#//
  [CreateAssetMenu(menuName = "Examples/Decouple/ServiceExample/Context", fileName = "ServiceExampleContext")]
  public class ServiceExampleContext : Services<ServiceExampleServiceAdapter, ServiceExampleContext>.Context {
    /// <a href=""></a> //#TBD#//
    protected bool Equals(ServiceExampleContext other) => base.Equals(other);
  }
}
#endif