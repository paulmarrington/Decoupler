// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using System.IO;
using Askowl;
using UnityEditor;
using UnityEngine;

namespace Decoupler {
  /// <a href=""></a> //#TBD#//
  public class TemplateWizard : AssetWizard {
    private static TemplateWizard newServiceInputForm;

    [MenuItem("Assets/Create/Decoupled/_Template_/New Service")] private static void Start() {
      if (newServiceInputForm == null) newServiceInputForm = CreateInstance<TemplateWizard>();
      Selection.activeObject = newServiceInputForm;
    }

    [SerializeField] private string newTemplateServiceName;

    private string destinationPath;

    protected override void Clear() => newTemplateServiceName = "";

    protected override void Create() {
      assetType       = "/*-assetType-*/";
      destination     = "/*-destination-*/";
      destinationName = "/*-destinationName-*/";
      var serviceFor = $"{destination}/TemplateServiceFor";
      var fileName   = $"{serviceFor}{newTemplateServiceName}.cs";
      File.Copy($"{serviceFor}.cs", fileName);
      ProcessFiles("cs", fileName);
    }

    protected override string FillTemplate(Template template, string text) =>
      template.From(text).Substitute("_ConcreteService_", newTemplateServiceName).Result();

    protected override void OnScriptReload() =>
      CreateAssetDictionary((newTemplateServiceName, Type.GetType($"Decoupler.Services.{newTemplateServiceName}")));
  }
}