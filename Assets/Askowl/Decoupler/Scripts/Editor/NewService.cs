// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using System.Text.RegularExpressions;
using Askowl;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Decoupler {
  public class NewService : AssetWizard {
    [MenuItem("Assets/Create/Decoupled/New Service")] private static void Start() {
      Selection.activeObject = wizard.Value;
      EditorGUI.FocusTextInControl("FirstWizardField");
    }
    private static readonly Jit<NewService> wizard = Jit<NewService>.Instance(
      _ => AssetDb.LoadOrCreate<NewService>("Askowl/Decoupler/Scripts/Editor/NewService.asset"));

    [SerializeField] private string        newServiceName;
    [SerializeField] private string        context = "RuntimePlatform platform;";
    [SerializeField] private ServiceMeta[] entryPoints;

    [Serializable] private struct ServiceMeta {
      [SerializeField] internal string entryPointName;
      [SerializeField] internal string requestData;
      [SerializeField] internal string responseData;
      public ServiceMeta(string to = default) => entryPointName = requestData = responseData = to;
    }

    public void AddEntryPoint(string entryPointName, string request, string response) {
      var meta = new ServiceMeta[entryPoints.Length + 1];
      entryPoints.CopyTo(meta, 0);
      meta[entryPoints.Length] = new ServiceMeta
        {entryPointName = entryPointName, requestData = request, responseData = response};
      entryPoints = meta;
    }

    public override void Clear(string dest = "") {
      destinationPath = dest;
      newServiceName  = context = "";
      entryPoints     = new ServiceMeta[0];
//      LayerMask layerMask = LayerMask.GetMask("default");
//      Physics.Raycast(new Ray(), maxDistance: 10, layerMask);
    }

    public override void Create() => CreateAssets(newAssetType: "Decoupler", key: "Decoupled.NewService");

    protected override string FillTemplate(Template template, string text) {
      template.From(text);
      Regex re(string regex) =>
        new Regex(regex.Replace("/*-", @"/\*-").Replace("-*/", @"-\*/"), RegexOptions.Singleline);

      var pairs = ToDefinitions(context);
      using (var inner = template.Inner(re("/*-ContextField...-*/(.*?)/*-...ContextField-*/"))) {
        while (inner.More())
          for (int i = 0; i < (pairs.Length - 1); i += 2)
            inner.Substitute(re("string /*-contextFieldType-*/"), pairs[i]).And("contextFieldName", pairs[i + 1]).Add();
      }
      using (var inner = template.Inner(re("/*-ContextEquals-(.*?)-*/"))) {
        while (inner.More())
          for (int i = 0; i < (pairs.Length - 1); i += 2)
            inner.Substitute("ContextEquality", pairs[i + 1]).Add();
      }

      using (var inner = template.Inner(re("/*-EntryPoint...-*/(.*?)/*-...EntryPoint-*/"))) {
        while (inner.More())
          for (int i = 0; i < entryPoints.Length; i++)
            if (!string.IsNullOrWhiteSpace(entryPoints[i].entryPointName)) {
              inner.Substitute("EntryPoint", entryPoints[i].entryPointName);
              if (!string.IsNullOrWhiteSpace(entryPoints[i].requestData))
                inner.And(re("int /*-entryPointRequest-*/"), ToTuple(entryPoints[i].requestData) ?? "string");
              if (!string.IsNullOrWhiteSpace(entryPoints[i].responseData))
                inner.And(re("int /*-entryPointResponse-*/"), ToTuple(entryPoints[i].responseData) ?? "string");
              inner.Add();
            }
      }

      return template.Substitute("_Template_", newServiceName)
                     .And(re("/*-destination-*/"), destination)
                     .And(re("/*-assetType-*/"),   assetType)
                     .And(re("/*--"),              "")
                     .And(re("--*/"),              "")
                     .Result();
    }

    protected override string GetDestinationPath(string basePath) => $"{basePath}/{newServiceName}";

    [DidReloadScripts] private static void OnScriptReload() {
      using (var assets = AssetEditor.Instance(key: "Decoupled.NewService")) {
        if (assets == null) return;
        var split = assets.destination.Split('/');
        destination = split[split.Length - 1].Trim();
        Environment mockEnvironment =
          AssetDatabase.LoadAssetAtPath<Environment>("Assets/Askowl/Decoupler/Scripts/Environments/Mock.asset");
        assets.Add(
                 ("ServicesManager", $"Decoupler.Services.{destination}ServicesManager")
               , ("ContextAsset", $"Decoupler.Services.{destination}Context")
               , ("ServiceForMock", $"Decoupler.Services.{destination}ServiceForMock")
               , ("Wizard", $"Decoupler.Services.{destination}Wizard"))
              .SetFieldToAssetEditorEntry("ServicesManager", "context", "ContextAsset")
              .SetFieldToAssetEditorEntry("ServiceForMock",  "context", "ContextAsset")
              .InsertIntoArrayField("ServicesManager", "services", "ServiceForMock")
              .SetField("ContextAsset", "environment", mockEnvironment);
        Debug.Log("      ...All Done");
      }
    }
  }
}