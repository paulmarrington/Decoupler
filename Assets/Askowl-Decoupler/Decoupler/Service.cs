using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Decoupled {

  public class Service<T> where T : Service<T>, new() {

    static T instance = default(T);
    static bool available = false;

    public bool Available{ get { return available; } }

    public static T Instance {
      get {
        if (instance == null) {
          instance = new T ();
          Debug.LogWarning("Service '" + instance.GetType().Name + "' not implemented");
          available = false;
        }
        return instance;
      }
    }

    public static IEnumerator Create() {
      instance = new T ();
      available = true;
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
