#if TextMeshPro
using TMPro;
using UnityEngine;

namespace Decoupled {
  [RequireComponent(typeof(TextMeshProUGUI))]
  public partial class Textual {
    private TextMeshProUGUI tmpText;

    [RuntimeInitializeOnLoadMethod]
    private static void TextMeshProInitialise() {
      // ReSharper disable once SuspiciousTypeConversion.Global
      initialisers += (my) => interfaceData = interfaceData ?? my.GetComponent<TextMeshProUGUI>();
    }
  }
}
#endif