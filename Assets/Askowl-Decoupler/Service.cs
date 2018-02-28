using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decoupled {
  using JetBrains.Annotations;

  public class Service<T> where T : Service<T>, new() {
    //      : UnityEngine.Object
    private static List<T> instanceList;
    private static Dictionary<string,T> instanceDictionary;
    private static T defaultInstance;
    private static Selector<T> selector;

    private static bool Available{ get { return instanceList.Count > 0; } }

    static Service() {
      Reset();
    }

    public static void Reset() {
      instanceList = new List<T> ();
      instanceDictionary = new Dictionary<string,T> ();
      defaultInstance = default(T);
      selector = new Selector<T> ();
      selector.Cycle();
    }

    [UsedImplicitly]
    public static void Random() {
      selector.Random();
    }

    [UsedImplicitly]
    public static void Exhaustive() {
      selector.Exhaustive();
    }

    public static T Instance {
      get {
        if (!Available) {
          if (defaultInstance == default(T)) {
            Debug.LogWarning("Service '" + typeof(T).Name + "' does not have an implemention");
            defaultInstance = new T();
            if (defaultInstance == null) {
              Debug.LogError("Cannot instantiate default '" + typeof(T).Name + "'");
            }
          }
          return defaultInstance;
        }
        return selector.Pick();
      }
    }

    [UsedImplicitly]
    public static T Fetch([NotNull] string name) {
      return instanceDictionary [name];
    }

    public static IEnumerator Register<D>([CanBeNull] string name = null) where D : T, new() {
      yield return Load<D>().Initialise();
    }

    [NotNull]
    public static T Load<D>(string name = null) where D : T, new() {
      T instance = new D ();
      name = name ?? instance.GetType().Name;
      instanceList.Add(instance);
      instanceDictionary.Add(name, instance);
      selector.Choices = instanceList.ToArray();
      return instance;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    protected IEnumerator Initialise() {
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator Destroy() {
      yield return null;
    }

  }
}