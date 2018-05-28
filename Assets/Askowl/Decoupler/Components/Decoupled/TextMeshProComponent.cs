// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

#if TextMeshPro
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Decoupled {
  public partial class Textual {
    private class TextMeshProUguiInterface : Interface {
      private TextMeshProUGUI uiText;

      public string text { get { return uiText.text; } set { uiText.text = value; } }

      public static Interface Instance(Textual textual) {
        TextMeshProUGUI uiText = textual.GetComponent<TextMeshProUGUI>();
        textual.DefaultComponent = typeof(TextMeshProUGUI);
        return (uiText == null) ? null : new TextMeshProUguiInterface {uiText = uiText};
      }
    }

    [RuntimeInitializeOnLoadMethod, InitializeOnLoadMethod]
    private static void TextMeshProInitialise() {
      Initialisers += (textual) => textual.backer = TextMeshProUguiInterface.Instance(textual);
    }
  }
}
#endif