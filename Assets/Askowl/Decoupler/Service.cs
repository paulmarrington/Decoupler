namespace Decoupled {
  using System;
  using System.Collections.Generic;
  using UnityEngine;

  /// <a href=""></a> //#TBD#//
  public abstract class Service {
    /// <a href=""></a> //#TBD#//
    public string Name;

    /// <a href=""></a> //#TBD#//
    protected static bool HasMockInstance;

    /// <a href=""></a> //#TBD#//
    protected virtual void Initialise() { }

    /// <a href=""></a> //#TBD#//
    public static void RegisterAsMock(Service mock) { mock.RegisterAsMock(); }

    /// <a href=""></a> //#TBD#//
    protected abstract void RegisterAsMock();
  }

  /// <a href=""></a> //#TBD#// <inheritdoc />
  public class Service<T> : Service where T : Service<T>, new() {
    private static readonly List<T> instanceList = new List<T>();

    private static T defaultInstance;

    private static bool Available => instanceList.Count > 0;

    internal static void Reset() {
      instanceList.Clear();
      defaultInstance = default;
    }

    /// <a href=""></a> //#TBD#//
    public static T Instance {
      get {
        if (Available) return instanceList[0];
        if (defaultInstance != default(T)) return defaultInstance;

        defaultInstance = new T();

        if (defaultInstance == null) { Debug.LogErrorFormat("Cannot instantiate default '{0}'", typeof(T).Name); }
        else { defaultInstance.Initialise(); }

        return defaultInstance;
      }
    }

    /// <a href=""></a> //#TBD#//
    public static T NamedInstance(string name) {
      for (var i = 0; i < instanceList.Count; i++) {
        if (instanceList[i].Name == name) return instanceList[i];
      }

      return null;
    }

    /// <a href=""></a> //#TBD#//
    public static void ForEach(Action<T> action) {
      for (var i = 0; i < instanceList.Count; i++) action(instanceList[i]);
    }

    /// <a href=""></a> //#TBD#//
    public static void RegisterDefault<Td>() where Td : T, new() {
      if (Available) return;

      defaultInstance = new Td { Name = typeof(Td).Name };
    }

    /// <a href=""></a> //#TBD#//
    public static void Register<Td>() where Td : T, new() {
      if (Available) Debug.LogWarningFormat("More than one implementation for service '{0}'", typeof(T).Name);

      if (!HasMockInstance) new Td { Name = typeof(Td).Name }.Register();
    }

    internal void Register() {
      instanceList.Add((T) this);
      Initialise();
    }

    /// <a href=""></a> //#TBD#// <inheritdoc />
    protected override void RegisterAsMock() {
      Reset();
      HasMockInstance = true;
      Register();
    }
  }
}