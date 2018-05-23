// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Decoupled {
  /// <inheritdoc />
  /// <summary>
  /// Interface to generic UI text functions. Defaults to <see cref="UnityEngine.UI.Text"/>
  /// </summary>
  /// <remarks><a href="http://decoupler.marrington.net#decoupledtext">More...</a></remarks>
  public partial class Textual : ComponentDecoupler<Textual> {
    /// <summary>
    /// Interface for Textual Objects
    /// </summary>
    [UsedImplicitly]
    public interface Interface {
      // ReSharper disable once InconsistentNaming
      /// <summary>
      /// Get or set the text value displayed by the component
      /// </summary>
      string text { get; set; }
    }

    private static Interface Backer { get { return (Interface) InterfaceData; } }

    /// <inheritdoc />
    protected override Type DefaultComponent { get { return typeof(Text); } }

    /// <summary>
    /// Get and set text in backing component
    /// </summary>
    public string text { [UsedImplicitly] get { return Backer.text; } set { Backer.text = value; } }
  }

  public partial class Textual {
    [RuntimeInitializeOnLoadMethod, InitializeOnLoadMethod]
    private static void UnityTextInitialise() {
      // ReSharper disable once SuspiciousTypeConversion.Global
      Initialisers += (my) => InterfaceData = InterfaceData ?? my.GetComponent<Text>() as Interface;
    }
  }
}