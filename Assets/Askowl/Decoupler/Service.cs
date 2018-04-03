namespace Decoupled {
  using System.Collections;
  using System.Collections.Generic;
  using Askowl;
  using JetBrains.Annotations;
  using UnityEngine;

  public class Service<T> where T : Service<T>, new() {
    public static List<T> InstanceList;

    private static Dictionary<string, T> instanceDictionary;
    private static T                     defaultInstance;
    private static Selector<T>           selector;

    private static bool Available { get { return InstanceList.Count > 0; } }

    static Service() { Reset(); }

    public static void Reset() {
      InstanceList       = new List<T>();
      instanceDictionary = new Dictionary<string, T>();
      defaultInstance    = default(T);
      selector           = new Selector<T>();
      selector.Cycle();
    }

    [UsedImplicitly]
    public static void Random() { selector.Random(); }

    [UsedImplicitly]
    public static void Exhaustive() { selector.Exhaustive(); }

    public static T Instance {
      get {
        if (Available) return selector.Pick();

        if (defaultInstance != default(T)) return defaultInstance;

        Debug.LogWarning(message: "Service '" + typeof(T).Name +
                                  "' does not have an implemention");

        defaultInstance = new T();

        if (defaultInstance == null) {
          Debug.LogError(message: "Cannot instantiate default '" + typeof(T).Name + "'");
        }

        return defaultInstance;
      }
    }

    [UsedImplicitly]
    public static T Fetch([NotNull] string name) { return instanceDictionary[key: name]; }

    public static IEnumerator Register<TD>() where TD : T, new() {
      yield return Load<TD>().Initialise();
    }

    [NotNull]
    public static T Load<TD>(string name = null) where TD : T, new() {
      T instance = new TD();
      name             = name ?? instance.GetType().Name;
      InstanceList.Add(item: instance);
      instanceDictionary.Add(key: name, value: instance);
      selector.Choices = InstanceList.ToArray();
      return instance;
    }

    [UsedImplicitly]
    protected virtual IEnumerator Initialise() { yield return null; }

    [UsedImplicitly]
    public virtual IEnumerator Destroy() { yield return null; }
  }
}