// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using UnityEngine;

namespace Decoupled {
  public class ComponentInterface {
    internal Component Component;
  }

  /// <inheritdoc />
  /// <summary>
  /// Superclass for a decoupled component Monobehaviour
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class ComponentDecoupler<T> : MonoBehaviour where T : ComponentDecoupler<T> {
    /// <summary>
    /// Actions set by alternative components to see who is boss
    /// </summary>
    protected static event Action<T> Initialisers = delegate { };

    protected ComponentInterface componentInterface;

    /// <summary>
    /// Retrieve the component type of the default component
    /// </summary>
    protected Type DefaultComponent;

    private void Awake() { Reset(); }

    /// <summary>
    /// Called when component is loaded or reset from the Inspector menu
    /// </summary>
    protected void Reset() {
      Initialisers(this as T);

      if ((componentInterface == null) || (DefaultComponent == null)) return;

      gameObject.AddComponent(DefaultComponent);
      Initialisers(this as T);
    }

    protected static void Instantiate<TI, TC>(bool primary)
      where TC : Component where TI : ComponentInterface, new() {
      Initialisers += (textual) => {
        if (textual.componentInterface != null) return;

        if (primary || (textual.DefaultComponent == null)) textual.DefaultComponent = typeof(TC);

        TC component = textual.GetComponent<TC>();

        if (component != null) textual.componentInterface = new TI {Component = component};
      };
    }
  }
}