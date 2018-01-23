using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Decoupled {

  public class Service<T> : UnityEngine.Object where T : Service<T>, new() {

    static List<T> instanceList;
    static Dictionary<string,T> instanceDictionary;
    static T defaultInstance;
    static AssetSelector<T> selector;

    public static bool Available{ get { return instanceList.Count > 0; } }

    static Service() {
      Reset();
    }

    public static void Reset() {
      instanceList = new List<T> ();
      instanceDictionary = new Dictionary<string,T> ();
      defaultInstance = default(T);
      selector = AssetSelector<T>.Singleton();
      selector.Cycle();
    }

    public static void Random() {
      selector.Random();
    }

    public static void Exhaustive() {
      selector.Exhaustive();
    }

    public static T Instance {
      get {
        if (!Available) {
          if (defaultInstance == null) {
            defaultInstance = new T ();
            Debug.LogWarning("Service '" + typeof(T).Name + "' does not have an implemention");
          }
          return defaultInstance;
        }
        return selector.Pick();
      }
    }

    public static T Fetch(string name) {
      return instanceDictionary [name];
    }

    public static IEnumerator Register(string name = null) {
      T instance = new T ();
      name = (name == null) ? instance.GetType().Name : name;
      instanceList.Add(instance);
      instanceDictionary.Add(name, instance);
      selector.assets = instanceList.ToArray();
      yield return instance.Initialise();
    }

    public virtual IEnumerator Initialise() {
      yield return null;
    }

    public virtual IEnumerator Destroy() {
      yield return null;
    }
  }
}
