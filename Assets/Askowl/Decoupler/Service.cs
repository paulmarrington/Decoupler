﻿using System;
using JetBrains.Annotations;

namespace Decoupled {
  using System.Collections.Generic;
  using UnityEngine;

  /// <summary>
  /// Base class for decoupled interfaces. Provides constant services to register and access service networks.
  /// </summary>
  /// <typeparam name="T">use `public class Analytics : Service&lt;Analytics>{}` to define a service</typeparam>
  public class Service<T> where T : Service<T>, new() {
    /// <summary>
    /// Name for this concrete service - generated from the concrete interface class name
    /// </summary>
    [UsedImplicitly]
    public string Name { get; private set; }

    private static readonly List<T> InstanceList = new List<T>();

    private static T defaultInstance;

    private static bool Available { get { return InstanceList.Count > 0; } }

    /// <summary>
    /// Used by testing framework between tests
    /// </summary>
    public static void Reset() {
      InstanceList.Clear();
      defaultInstance = default(T);
    }

    /// <summary>
    /// Used to access a decoupled instance of the service - or a default one if none are registered
    /// </summary>
    public static T Instance {
      get {
        if (Available) return InstanceList[0];

        if (defaultInstance != default(T)) return defaultInstance;

        Debug.LogWarningFormat("Service '{0}' does not have an implemention", typeof(T).Name);

        defaultInstance = new T();

        if (defaultInstance == null) {
          Debug.LogError(message: "Cannot instantiate default '" + typeof(T).Name + "'");
        }

        return defaultInstance;
      }
    }

    /// <summary>
    /// A rare form of service interface will have more than one service and the application will
    /// refer to it by name if needed. An examples is `Authentication` where there may be a login button
    /// if and only if the service has been included in the build - which may well be platform dependent.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [UsedImplicitly]
    public static T Named(string name) {
      for (int i = 0; i < InstanceList.Count; i++) {
        if (InstanceList[i].Name == name) return InstanceList[i];
      }

      return null;
    }

    /// <summary>
    /// Another type of service is have multiple instances and we will need to do something with all of them.
    /// It could be anything from display a list of names for user selection or call a method on some or all of them.
    /// `Social` is one of these where we may be connected to multiple social networks and send a message to some.
    /// </summary>
    /// <param name="action"></param>
    [UsedImplicitly]
    public static void ForEach(Action<T> action) {
      for (int i = 0; i < InstanceList.Count; i++) action(InstanceList[i]);
    }

    /// <summary>
    /// Used to register a service implementation. Typically called within a `#if` set by service discovery by Unity editor code
    /// </summary>
    /// <typeparam name="TD">Type of the concrete service interface to register</typeparam>
    /// <returns>a reference to the instance</returns>
    public static void Register<TD>() where TD : T, new() {
      if (Available) {
        Debug.LogWarningFormat("More than one implementation for service '{0}'", typeof(T).Name);
      }

      TD service = new TD();
      service.Name = typeof(TD).Name;
      InstanceList.Add(service);
    }
  }
}