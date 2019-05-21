// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Askowl;
using Decoupler.Services;
using UnityEditor;
using UnityEngine;

namespace Decoupler {
  /// <a href="http://bit.ly/2OZP7gP">Build assets for all decoupler wizards run so far</a>
  public class BuildAssets : AssetWizard {
    /// <a href="http://bit.ly/2OZP7gP">Display the build assets form - only shown after decoupler wizard</a>
    public static void Display() {
      Debug.Log(
        "When ready, build associated assets with menu <color=blue>Assets/Create/Decoupled/Build Assets</color>");
      Selection.activeObject = wizard.Value;
      EditorGUI.FocusTextInControl("FirstWizardField");
    }
    private static readonly Jit<BuildAssets> wizard = Jit<BuildAssets>.Instance(
      _ => AssetDb.LoadOrCreate<BuildAssets>("Askowl/Decoupler/Scripts/Editor/BuildAssets.asset"));

    public override void Clear() => Display();

    /// <a href="http://bit.ly/2OZP7gP">Menu item that builds assets immediately (without form)</a>
    [MenuItem("Assets/Create/Decoupled/Build Assets")] public static void Start() => wizard.Value.Create();

    public override void Create() {
      if (LogEntries.HasErrors()) throw new Exception("Please fix compile errors first");
      var files = AssetDb.FindByLabel("BuildNewService");
      using (var assets = AssetEditor.Instance) {
        var assetNames = new List<string>();
        foreach (string filePath in files) {
          var dir        = Path.GetDirectoryName(filePath);
          var assetName  = Path.GetFileNameWithoutExtension(filePath);
          var scriptPath = $"{dir}/{assetName}.cs";
          var monoScript = AssetDb.Load<MonoScript>(scriptPath);
          var assetPath  = $"{dir}/{assetName}.asset";
          if (!File.Exists(assetPath)) {
            Debug.Log($"Building {assetPath}");
            assetNames.Add(assetName);
            var match     = namespaceRegex.Match(monoScript.text);
            var nameSpace = match.Success ? match.Groups[1].Value : "";
            assets.Add(assetName, nameSpace, assetPath);
          }
          AssetDatabase.ClearLabels(monoScript);
        }

        Environment mockEnvironment =
          AssetDatabase.LoadAssetAtPath<Environment>("Assets/Askowl/Decoupler/Scripts/Environments/Mock.asset");

        foreach (var assetName in assetNames) {
          var assetPath = assets.AssetPath(assetName);
          var path      = Path.GetDirectoryName(assetPath);
          var nameBase  = Path.GetFileNameWithoutExtension(path);
          switch (assets.Asset(assetName)) {
            case IServicesManager _:
              assets
               .SetFieldToAssetEditorEntry(assetName, "context", $"{nameBase}Context")
               .InsertIntoArrayField(assetName, "services", $"{nameBase}ServiceForMock");
              break;
            case IContext _:
              assets.SetField(assetName, "environment", mockEnvironment);
              break;
            case IServiceAdapter _:
//              var contextName = $"{nameBase}Context";
//              if (!assets.Exists(contextName)) assets.Load(contextName, Namespace(assetPath), path);
//              assets.SetFieldToAssetEditorEntry(assetName, "context", $"{nameBase}Context");
              break;
          }
        }
      }
    }
    private static readonly Regex namespaceRegex = new Regex(@"^\s*namespace (\S+)", RegexOptions.Multiline);
  }
}