// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;

namespace Decoupled {
  /// <summary>
  /// Use as superclass for Interface specifications
  /// </summary>
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

    private static HashSet<Type> interfaces = new HashSet<Type>();

    /// <summary>
    /// Interface created in concrete classes to provide decoupled component access
    /// </summary>
    protected ComponentInterface ComponentInterface;

    /// <summary>
    /// Retrieve the component type of the default component
    /// </summary>
    private Type defaultComponent;

    private void Start() { Reset(); }

    /// <summary>
    /// Called when component is loaded or reset from the Inspector menu
    /// </summary>
    protected void Reset() {
      ComponentInterface = null;
      Initialisers(this as T);

      if ((ComponentInterface != null) || (defaultComponent == null)) return;

      gameObject.AddComponent(defaultComponent);
      Initialisers(this as T);
    }

    /// <summary>
    /// Called on load to find and assign a component found in the current GameObject.
    /// If not found, set the default for adding one later. Use by editor and runtime.
    /// </summary>
    /// <param name="primary">True if this component becomes the one created if none found</param>
    /// <typeparam name="TI">Type of the interface used to access this component</typeparam>
    /// <typeparam name="TC">Type of component we are playing with</typeparam>
    protected static void Instantiate<TI, TC>(bool primary)
      where TC : Component where TI : ComponentInterface, new() {
      Type type = typeof(TI);
      if (interfaces.Contains(type)) return;

      interfaces.Add(type);

      Initialisers += (textual) => {
        if (textual.ComponentInterface != null) return;

        if (primary || (textual.defaultComponent == null)) textual.defaultComponent = typeof(TC);

        TC component = textual.GetComponent<TC>();

        if (component != null) textual.ComponentInterface = new TI {Component = component};
      };
    }
  }
}