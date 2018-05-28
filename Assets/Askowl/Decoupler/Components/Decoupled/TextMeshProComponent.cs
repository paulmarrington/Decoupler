// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

#if TextMeshPro
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Decoupled {
  public partial class Textual {
    private class TextMeshProUguiInterface : ComponentInterface, TextualInterface {
      private TextMeshProUGUI TmpText { get { return Component as TextMeshProUGUI; } }

      public string text { get { return TmpText.text; } set { TmpText.text = value; } }
    }

    [RuntimeInitializeOnLoadMethod, InitializeOnLoadMethod]
    private static void TextMeshProUguiInitialise() {
      Instantiate<TextMeshProUguiInterface, TextMeshProUGUI>(primary: true);
    }
  }
}
#endif