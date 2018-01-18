using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Decoupled {

  public class Service<T> where T : Service<T>, new() {

    static List<T> instanceList = new List<T> ();
    static Dictionary<string,T> instanceDictionary = new Dictionary<string,T> ();
    static T defaultInstance = null;

    public static bool Available{ get { return instanceList.Count > 0; } }

    public static T Instance {
      get {
        if (!Available) {
          if (defaultInstance == null) {
            defaultInstance = new T ();
            Debug.LogWarning("Service '" + instance.GetType().Name + "' does not have an implemention");
            available = false;
          }
        }
        return instanceList [0];
      }
    }

    public static IEnumerator Create() {
      string name = typeof(T).Name;
      T instance = new T ();
      instanceList.Add(instance);
      yield return instance.Initialise();
    }

    Service() {
    }

    public virtual IEnumerator Initialise() {
      yield return null;
    }

    public virtual IEnumerator Destroy() {
      yield return null;
    }
  }
}
