// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Decoupled {
  internal interface TextualInterface {
    // ReSharper disable once InconsistentNaming
    string text { get; set; }
  }

  /// <inheritdoc cref="MonoBehaviour" />
  /// <summary>
  /// Interface to generic UI text functions. Defaults to <see cref="UnityEngine.UI.Text"/>
  /// </summary>
  /// <remarks><a href="http://decoupler.marrington.net#decoupledtext">More...</a></remarks>
  public partial class Textual : ComponentDecoupler<Textual>, TextualInterface {
    private TextualInterface Backer { get { return ComponentInterface as TextualInterface; } }

    /// <summary>
    /// Get and set text in backing component
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public string text { [UsedImplicitly] get { return Backer.text; } set { Backer.text = value; } }
  }

  public partial class Textual {
    private class UnityTextInterface : ComponentInterface, TextualInterface {
      private Text UnityText { get { return Component as Text; } }

      public string text { get { return UnityText.text; } set { UnityText.text = value; } }
    }

    [RuntimeInitializeOnLoadMethod, InitializeOnLoadMethod]
    private static void UnityTextInitialise() { Instantiate<UnityTextInterface, Text>(false); }
  }
}