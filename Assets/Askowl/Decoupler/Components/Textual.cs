// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Decoupled {
  internal interface TextualInterface {
    string text { get; set; }
  }

  /// <inheritdoc cref="MonoBehaviour" />
  /// <summary>
  /// Interface to generic UI text functions. Defaults to <see cref="UnityEngine.UI.Text"/>
  /// </summary>
  /// <remarks><a href="http://decoupler.marrington.net#decoupledtext">More...</a></remarks>
  public partial class Textual : ComponentDecoupler<Textual>, TextualInterface {
    private TextualInterface Backer { get { return Instance as TextualInterface; } }

    /// <summary>
    /// Get and set text in backing component
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public string text { get { return Backer.text; } set { Backer.text = value; } }
  }

  public partial class Textual {
    private class UnityTextInterface : ComponentInterface, TextualInterface {
      private Text UnityText { get { return Component as Text; } }

      public string text { get { return UnityText.text; } set { UnityText.text = value; } }

      public UnityTextInterface() { Instantiate<Text>(false); }
    }

    private static UnityTextInterface unityTextInterface = new UnityTextInterface();
  }
}