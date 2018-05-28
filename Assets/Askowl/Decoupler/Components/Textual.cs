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
    public interface Interface {
      // ReSharper disable once InconsistentNaming
      /// <summary>
      /// Get or set the text value displayed by the component
      /// </summary>
      string text { get; set; }
    }

    private Interface backer { get { return componentInterface as Interface; } }

    /// <summary>
    /// Get and set text in backing component
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public string text { [UsedImplicitly] get { return backer.text; } set { backer.text = value; } }
  }

  public partial class Textual {
    private class UnityTextInterface : ComponentInterface, Interface {
      private Text UnityText { get { return Component as Text; } }

      public string text { get { return UnityText.text; } set { UnityText.text = value; } }
    }

    [RuntimeInitializeOnLoadMethod, InitializeOnLoadMethod]
    private static void UnityTextInitialise() { Instantiate<UnityTextInterface, Text>(false); }
  }
}