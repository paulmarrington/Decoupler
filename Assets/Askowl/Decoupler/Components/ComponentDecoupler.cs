using System;
using UnityEngine;

namespace Decoupled {
  public abstract class ComponentDecoupler<T> : MonoBehaviour where T : ComponentDecoupler<T> {
    protected static event Action<T> initialisers = delegate { };
    protected static   object        interfaceData;
    protected abstract Type          defaultComponent { get; }

    protected void Reset() {
      initialisers(this as T);
      if ((interfaceData != null) || (defaultComponent == null)) return;

      gameObject.AddComponent(defaultComponent);
      initialisers(this as T);
    }
  }
}