﻿// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

namespace Decoupled {
  using System;
  using System.Collections.Generic;
  using UnityEngine;

  /// <a href=""></a> //#TBD#// <inheritdoc />
  public abstract class ComponentDecoupler<T> : MonoBehaviour where T : ComponentDecoupler<T> {
    /// <a href=""></a> //#TBD#//
    protected static event Action<T> Initialisers = delegate { };

    // ReSharper disable once StaticMemberInGenericType
    private static readonly HashSet<Type> interfaces = new HashSet<Type>();

    /// <a href=""></a> //#TBD#//
    public class ComponentInterface {
      internal Component Component;

      /// <a href=""></a> //#TBD#//
      protected void Instantiate<Tc>(bool primary) where Tc : Component {
        Type type = typeof(Tc);
        if (interfaces.Contains(type)) return;

        interfaces.Add(type);

        void initialiser(T t) {
          if (t.Instantiated) return; // someone else got in first

          if (primary || (t.defaultComponent == null)) t.defaultComponent = type;

          var component = t.GetComponent<Tc>();
          if (component != null) {
            Component            = component;
            t.componentInterface = (ComponentInterface) MemberwiseClone();
          }
        }

        Initialisers += initialiser;
      }

      /// <a href=""></a> //#TBD#//
      public override string ToString() => Component == null ? "null" : Component.gameObject.name;
    }

    private ComponentInterface componentInterface;

    private Type defaultComponent;

    /// <a href=""></a> //#TBD#//
    protected bool Instantiated => componentInterface != null;

    /// <a href=""></a> //#TBD#//
    protected ComponentInterface Instance {
      get {
        if (componentInterface != null) return componentInterface;

        Initialisers(this as T);
        if ((componentInterface != null) || (defaultComponent == null)) return componentInterface;

        gameObject.AddComponent(defaultComponent);
        Initialisers(this as T);
        return componentInterface;
      }
    }
  }
}