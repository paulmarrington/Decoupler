// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using System.IO;
using Askowl;
using UnityEditor;
using UnityEngine;

namespace Decoupler {
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
      assetType = "/*-assetType-*/";
      CreateAssets("cs");
    }
    protected override bool ProcessAllFiles(string textAssetTypes) {
      var serviceFor = $"{destination}/_Template_ServiceFor";
      var fileName   = $"{serviceFor}{newTemplateServiceName}.cs";
      File.Copy($"{serviceFor}.cs", fileName);
      ProcessFiles("cs", fileName);
      return true;
    }
    /// <a href=""></a> //#TBD#//
    protected override string GetDestinationPath() => "/*-destination-*/";

    protected override string FillTemplate(Template template, string text) =>
      template.From(text)
              .Substitute("_ConcreteService_", newTemplateServiceName)
              .And("/\\*++", "").And("++\\*/", "").Result();

    protected override void OnScriptReload() =>
      CreateAssetDictionary((newTemplateServiceName, Type.GetType($"Decoupler.Services.{newTemplateServiceName}")));
  }
}