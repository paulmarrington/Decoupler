using System;

namespace Decoupled {
  using System.Collections.Generic;
  using UnityEngine;

  public abstract class Service {
    public string Name;

    protected static bool hasMockInstance;

    /// <summary>
    /// Override to initialise service when it is created.
    /// </summary>
    protected virtual void Initialise() { }

    public static void RegisterAsMock(Service mock) { mock.RegisterAsMock(); }

    protected abstract void RegisterAsMock();
  }

  /// <summary>
  /// Base class for decoupled interfaces. Provides constant services to register and access service networks.
  /// </summary>
  /// <remarks><a href="http://decoupler.marrington.net#for-a-new-interface">More...</a></remarks>
  /// <typeparam name="T">use `public class MyService : Service&lt;MyService>{}` to define a service</typeparam>
  public class Service<T> : Service where T : Service<T>, new() {
    private static readonly List<T> InstanceList = new List<T>();

    private static T defaultInstance;

    private static bool Available => InstanceList.Count > 0;

    /// <summary>
    /// Used by testing framework between tests
    /// </summary>
    internal static void Reset() {
      InstanceList.Clear();
      defaultInstance = default(T);
    }

    /// <summary>
    /// Used to access a decoupled instance of the service - or a default one if none are registered
    /// </summary>
    /// <remarks><a href="http://decoupler.marrington.net#for-singleton-services">More...</a></remarks>
    public static T Instance {
      get {
        if (Available) return InstanceList[0];
        if (defaultInstance != default(T)) return defaultInstance;

        defaultInstance = new T();

        if (defaultInstance == null) {
          Debug.LogErrorFormat("Cannot instantiate default '{0}'", typeof(T).Name);
        } else {
          defaultInstance.Initialise();
        }

        return defaultInstance;
      }
    }

    /// <summary>
    /// A rare form of service interface will have more than one service and the application will
    /// refer to it by name if needed. An examples is `Authentication` where there may be a login button
    /// if and only if the service has been included in the build - which may well be platform dependent.
    /// </summary>
    /// <remarks><a href="http://decoupler.marrington.net#to-select-a-named-service">More...</a></remarks>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T NamedInstance(string name) {
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
    /// <remarks><a href="http://decoupler.marrington.net#send-to-all-services">More...</a></remarks>
    /// <param name="action"></param>
    public static void ForEach(Action<T> action) {
      for (int i = 0; i < InstanceList.Count; i++) action(InstanceList[i]);
    }

    /// <summary>
    /// Used to register a service implementation. Typically called within a `#if` set by service discovery by Unity editor code
    /// </summary>
    /// <remarks><a href="http://decoupler.marrington.net#for-a-new-package-and-an-existing-interface">More...</a></remarks>
    /// <typeparam name="TD">Type of the concrete service interface to register</typeparam>
    /// <returns>a reference to the instance</returns>
    public static void RegisterDefault<TD>() where TD : T, new() {
      if (Available) return;

      defaultInstance = new TD {Name = typeof(TD).Name};
    }

    /// <summary>
    /// Used to register a service implementation. Typically called within a `#if` set by service discovery by Unity editor code
    /// </summary>
    /// <remarks><a href="http://decoupler.marrington.net#for-a-new-package-and-an-existing-interface">More...</a></remarks>
    /// <typeparam name="TD">Type of the concrete service interface to register</typeparam>
    /// <returns>a reference to the instance</returns>
    public static void Register<TD>() where TD : T, new() {
      if (Available) {
        Debug.LogWarningFormat("More than one implementation for service '{0}'", typeof(T).Name);
      }

      if (!hasMockInstance) (new TD {Name = typeof(TD).Name}).Register();
    }

    internal void Register() {
      InstanceList.Add((T) this);
      Initialise();
    }

    protected override void RegisterAsMock() {
      Reset();
      hasMockInstance = true;
      Register();
    }
  }
}