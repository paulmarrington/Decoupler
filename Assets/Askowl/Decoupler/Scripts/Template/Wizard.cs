// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using System.IO;
using Askowl;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Decoupler.Services {
  public class _Template_Wizard : AssetWizard {
    private static _Template_Wizard newServiceInputForm;

    /*--[MenuItem("Assets/Create/Decoupled/_Template_/New Concrete Service")]--*/
    private static void Start() {
      if (newServiceInputForm == null) newServiceInputForm = CreateInstance<_Template_Wizard>();
      Selection.activeObject = newServiceInputForm;
    }

    [SerializeField] private string new_Template_Name;

    protected override void Clear() => new_Template_Name = "";

    public override void Create() {
      assetType   = "/*-assetType-*/";
      destination = "/*-destination-*/";
      PlayerPrefs.SetString($"AssetWizard.CreateAssets._Template_", $"ServiceFor{new_Template_Name}");
      CreateAssets("Decoupler", "_Template_DecoupledService");
    }
    protected override bool ProcessAllFiles(string textAssetTypes) {
      var serviceFor = $"{destination}/_Template_ServiceFor";
      var fileName   = $"{serviceFor}{new_Template_Name}.cs";
      if (File.Exists(fileName)) throw new Exception($"'{fileName}' already exists. Try another name.");
      File.Copy($"{serviceFor}.cs", fileName);
      ProcessFiles("cs", fileName);
      return true;
    }

    /// <a href=""></a> //#TBD#//
    protected override string GetDestinationPath(string basePath) => basePath;

    protected override string FillTemplate(Template template, string text) =>
      template.From(text)
              .Substitute("_ConcreteService_", new_Template_Name)
              .And(@"/\*\+\+", "").And(@"\+\+\*/", "").Result();

    public override void OnScriptReload() {
      using (var assets = AssetEditor.Instance("_Template_DecoupledService")) {
        if (assets == null) return;
        var newTemplateServiceName = PlayerPrefs.GetString($"AssetWizard.CreateAssets._Template_");
        assets.Add((newTemplateServiceName, $"Decoupler.Services._Template_{newTemplateServiceName}"))
              .Load(("ContextAsset", "Decoupler.Services._Template_Context"))
              .SetFieldToAssetEditorEntry(newTemplateServiceName, "context", "ContextAsset");
        Debug.Log("...All Done");
      }
    }
  }
}