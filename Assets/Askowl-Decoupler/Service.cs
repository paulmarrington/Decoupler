using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Decoupled {

  public class Service : UnityEngine.Object {

    static List<Service> instanceList;
    static Dictionary<string,Service> instanceDictionary;
    static Service defaultInstance = null;
    static Selector<Service> selector;

    public static bool Available{ get { return instanceList.Count > 0; } }

    static Service() {
      Reset();
    }

    public static void Reset() {
      instanceList = new List<Service> ();
      instanceDictionary = new Dictionary<string,Service> ();
      defaultInstance = null;
      selector = new Selector<Service> ();
      selector.Cycle();
    }

    public static void Random() {
      selector.Random();
    }

    public static void Exhaustive() {
      selector.Exhaustive();
    }

    public static T Instance<T>() where T : Service, new() {
      if (!Available) {
        if (defaultInstance == null) {
          Debug.LogWarning("Service '" + typeof(T).Name + "' does not have an implemention");
          if ((defaultInstance = new T ()) == null) {
            Debug.LogError("Cannot instantiate default '" + typeof(T).Name + "'");
          }
        }
        return (T)defaultInstance;
      }
      return (T)selector.Pick();
    }

    public static T Fetch<T>(string name) where T : Service {
      return (T)instanceDictionary [name];
    }

    public static IEnumerator Register<T>(string name = null) where T : Service, new() {
      yield return Load<T>().Initialise();
    }

    public static T Load<T>(string name = null) where T : Service, new() {
      T instance = new T ();
      name = (name == null) ? instance.GetType().Name : name;
      instanceList.Add(instance);
      instanceDictionary.Add(name, instance);
      selector.Choices = instanceList.ToArray();
      return instance;
    }

    public virtual IEnumerator Initialise() {
      yield return null;
    }

    public virtual IEnumerator Destroy() {
      yield return null;
    }

  }
}
