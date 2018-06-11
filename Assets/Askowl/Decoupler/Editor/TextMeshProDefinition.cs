using CustomAsset;
using UnityEditor;

namespace Decoupled.Editor {
  /// <inheritdoc />
  /// <summary>
  /// Set definitions if TextMesh Pro is loaded
  /// </summary>
  [InitializeOnLoad]
  public class TextMeshProDefinition : DefineSymbols {
    static TextMeshProDefinition() {
      bool isLoaded = HasFolder("TextMesh Pro") || HasPackage("com.unity.textmeshpro");
      AddOrRemoveDefines(isLoaded, "TextMeshPro");
    }
  }
}