using System;
using System.IO;
using Askowl;
using UnityEditor;
using UnityEngine;

namespace Decoupler.Services {
  /*--[CreateAssetMenu(menuName = "Decoupled/_Template_/Concrete Service Wizard", fileName = "_Template_Wizard")]--*/
  public class _Template_Wizard : AssetWizard {
    private static _Template_Wizard newServiceInputForm;

    /*--[MenuItem("Assets/Create/Decoupled/_Template_/Add Concrete Service")]--*/
    internal static void Start() {
      if (newServiceInputForm == null) newServiceInputForm = CreateInstance<_Template_Wizard>();
      Selection.activeObject = newServiceInputForm;
    }

    [SerializeField] private string new_Template_Name;

    public override void Clear() => new_Template_Name = "";

    public override void Create() {
      type  = "/*-assetType-*/";
      Label = "BuildNewService";
      CreateAssets(assetName: "_Template_", assetType: "Decoupler", basePath: "/*-destination-*/");
      BuildAssets.Display();
    }
    protected override void ProcessAllFiles(string textAssetTypes) {
      var serviceFor = $"/*-destination-*//_Template_ServiceFor";
      var fileName   = $"{destination}/_Template_ServiceFor{new_Template_Name}.cs";
      if (File.Exists(fileName)) throw new Exception($"'{fileName}' already exists. Try another name.");
      File.Copy($"{serviceFor}.cs", fileName);
      ProcessFiles("cs", fileName);
    }

    protected override string FillTemplate(Template template, string text) =>
      template.From(text)
              .Substitute("_ConcreteService_", new_Template_Name)
              .And(@"/\*\+\+", "").And(@"\+\+\*/", "").Result();
  }
}