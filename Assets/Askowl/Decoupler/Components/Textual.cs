// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

namespace Decoupled {
  using UnityEngine.UI;

  internal interface TextualInterface {
    // ReSharper disable once InconsistentNaming
    string text { get; set; }
  }

  /// <a href=""></a> //#TBD#// <inheritdoc cref="ComponentDecoupler{T}" />
  public partial class Textual : ComponentDecoupler<Textual>, TextualInterface {
    private TextualInterface Backer => Instance as TextualInterface;

    /// <a href=""></a> //#TBD#//
    public string text { get => Backer.text; set => Backer.text = value; }
  }

  public partial class Textual {
    private class UnityTextInterface : ComponentInterface, TextualInterface {
      private Text UnityText => Component as Text;

      public string text { get => UnityText.text; set => UnityText.text = value; }

      public UnityTextInterface() { Instantiate<Text>(primary: false); }
    }

    // ReSharper disable once UnusedMember.Local
    private UnityTextInterface unityTextInterface = new UnityTextInterface();
  }
}