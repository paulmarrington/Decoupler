// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Decoupled {
  /// <inheritdoc />
  /// <summary>
  /// Superclass for a decoupled component MonoBehaviour
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class ComponentDecoupler<T> : MonoBehaviour where T : ComponentDecoupler<T> {
    /// <summary>
    /// Actions set by alternative components to see who is boss
    /// </summary>
    protected static event Action<T> Initialisers = delegate { };

    // ReSharper disable once StaticMemberInGenericType
    private static readonly HashSet<Type> Interfaces = new HashSet<Type>();

    /// <summary>
    /// Use as superclass for Interface specifications
    /// </summary>
    public class ComponentInterface {
      internal Component Component;

      /// <summary>
      /// Called on load to find and assign a component found in the current GameObject.
      /// If not found, set the default for adding one later. Use by editor and runtime.
      /// </summary>
      /// <param name="primary">True if this component becomes the one created if none found</param>
      /// <typeparam name="TC">Type of component we are playing with</typeparam>
      protected void Instantiate<TC>(bool primary)
        where TC : Component {
        Type type = GetType();
        if (Interfaces.Contains(type)) return;

        Interfaces.Add(type);

        Initialisers += (decoupler) => {
          if (decoupler.Instantiated) return;  // someone else got in first

          if (primary || (decoupler.defaultComponent == null)) {
            decoupler.defaultComponent = typeof(TC);
          }

          TC component = decoupler.GetComponent<TC>();

          if (component != null) {
            Component = component;
            decoupler.Prepare(this);
          }
        };
      }
    }

    /// <summary>
    /// Interface created in concrete classes to provide decoupled component access
    /// </summary>
    private ComponentInterface componentInterface;

    /// <summary>
    /// Retrieve the component type of the default component
    /// </summary>
    private Type defaultComponent;

    private void Awake() {
      componentInterface = null;
      Initialisers(this as T);

      if ((componentInterface != null) || (defaultComponent == null)) return;

      gameObject.AddComponent(defaultComponent);
      Initialisers(this as T);
    }

    internal void Prepare(ComponentInterface winner) { componentInterface = winner; }

    protected bool Instantiated { get { return componentInterface != null; } }

    protected ComponentInterface Instance { get { return componentInterface; } }
  }
}