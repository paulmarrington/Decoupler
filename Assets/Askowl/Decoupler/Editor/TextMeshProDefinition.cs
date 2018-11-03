namespace Decoupled.Editor {
  using Askowl;
  using UnityEditor;

  /// <a href=""></a> //#TBD#// <inheritdoc />
  [InitializeOnLoad] public class TextMeshProDefinition : DefineSymbols {
    static TextMeshProDefinition() {
      bool isLoaded = HasFolder("TextMesh Pro") || HasPackage("com.unity.textmeshpro");
      AddOrRemoveDefines(addDefines: isLoaded, named: "TextMeshPro");
    }
  }
}