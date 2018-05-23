#if TextMeshPro
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Decoupled {
  [RequireComponent(typeof(TextMeshProUGUI))]
  public partial class Textual {
    private TextMeshProUGUI tmpText;

    [RuntimeInitializeOnLoadMethod, InitializeOnLoadMethod]
    private static void TextMeshProInitialise() {
      // ReSharper disable once SuspiciousTypeConversion.Global
      Initialisers += (my) => InterfaceData = InterfaceData ?? my.GetComponent<TextMeshProUGUI>();
    }
  }
}
#endif