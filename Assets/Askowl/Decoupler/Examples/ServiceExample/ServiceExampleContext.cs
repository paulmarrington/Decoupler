// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages
#if AskowlTests

using UnityEngine;

namespace Decoupler.Services {
  /// <inheritdoc />
  [CreateAssetMenu(menuName = "Examples/Decouple/ServiceExample/Context", fileName = "ServiceExampleContext")]
  public class ServiceExampleContext : Services<ServiceExampleServiceAdapter, ServiceExampleContext>.Context { }
}
#endif