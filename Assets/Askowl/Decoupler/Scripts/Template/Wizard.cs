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

    [SerializeField] private string newTemplateServiceName;

    private string destinationPath;

    protected override void Clear() => newTemplateServiceName = "";

    protected override void Create() {
      assetType   = "/*-assetType-*/";
      destination = "/*-destination-*/";
      PlayerPrefs.SetString($"AssetWizard.CreateAssets._Template_", $"ServiceFor{newTemplateServiceName}");
      CreateAssets("cs", "_Template_");
    }
    protected override bool ProcessAllFiles(string textAssetTypes) {
      var serviceFor = $"{destination}/_Template_ServiceFor";
      var fileName   = $"{serviceFor}{newTemplateServiceName}.cs";
      if (File.Exists(fileName)) throw new Exception($"'{fileName}' already exists. Try another name.");
      File.Copy($"{serviceFor}.cs", fileName);
      ProcessFiles("cs", fileName);
      return true;
    }
    /// <a href=""></a> //#TBD#//
    protected override string GetDestinationPath() => null;

    protected override string FillTemplate(Template template, string text) =>
      template.From(text)
              .Substitute("_ConcreteService_", newTemplateServiceName)
              .And(@"/\*\+\+", "").And(@"\+\+\*/", "").Result();

    [DidReloadScripts] private static void Phase2() {
      using (var assets = AssetCreator.Instance("_Template_")) {
        if (assets == null) return;
        var newTemplateServiceName = PlayerPrefs.GetString($"AssetWizard.CreateAssets._Template_");
        assets.Add((newTemplateServiceName, $"Decoupler.Services._Template_{newTemplateServiceName}"))
              .Load(("ContextAsset", "Decoupler.Services._Template_Context"))
              .SetField(newTemplateServiceName, "context", "ContextAsset");
        Debug.Log("...All Done");
      }
    }
  }
}