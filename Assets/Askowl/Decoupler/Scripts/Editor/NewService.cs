// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using System.Text.RegularExpressions;
using Askowl;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Decoupler {
  /// <a href=""></a> //#TBD#//
  public class NewService : AssetWizard {
    [MenuItem("Assets/Create/Decoupled/New Service")] private static void Start() {
      var newServiceInputForm = Wizard;
//      EditorUtility.FocusProjectWindow();
      Selection.activeObject = newServiceInputForm;
      EditorGUI.FocusTextInControl("FirstWizardField");
    }

    private static NewService Wizard {
      get {
        if (wizard != default) return wizard;
        selectedPathInProjectView = AssetDatabase.GetAssetPath(Selection.activeObject);
        return wizard = LoadOrCreate<NewService>("Askowl/Decoupler/Scripts/Editor/NewService.asset");
      }
    }
    private static NewService wizard;

    [SerializeField] private string newServiceName;
    [SerializeField, Tooltip("C# definitions, as in 'String str;RuntimePlatform platform'")]
    private string context = "RuntimePlatform platform;";
    [SerializeField] private ServiceMeta[] entryPoints;

    /// <a href=""></a> //#TBD#//
    [Serializable] private struct ServiceMeta {
      [SerializeField] internal string entryPointName;
      [SerializeField] internal string requestData;
      [SerializeField] internal string responseData;
      public ServiceMeta(string to = default) => entryPointName = requestData = responseData = to;
    }

    protected override void Clear() {
      newServiceName = context = "";
      entryPoints    = default;
      var       ray       = new Ray();
      LayerMask layerMask = LayerMask.GetMask("default");
      Physics.Raycast(ray, maxDistance: 10, layerMask);
    }

    protected override void Create() => CreateAssets("Decoupler", "");

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
            inner.Substitute("EntryPoint", entryPoints[i].entryPointName)
                 .And(re("int /*-entryPointRequest-*/"),  ToTuple(entryPoints[i].requestData)  ?? "string")
                 .And(re("int /*-entryPointResponse-*/"), ToTuple(entryPoints[i].responseData) ?? "string")
                 .Add();
      }

      return template.Substitute("_Template_", newServiceName)
                     .And(re("/*-destination-*/"), destination)
                     .And(re("/*-assetType-*/"),   assetType)
                     .And(re("/*--"),              "")
                     .And(re("--*/"),              "")
                     .Result();
    }

    /// <a href=""></a> //#TBD#//
    protected override string GetDestinationPath() => $"{selectedPathInProjectView}/{newServiceName}";

    [DidReloadScripts] private static void Phase2() {
      using (var assets = AssetCreator.Instance("")) {
        if (assets == null) return;
        Environment mockEnvironment =
          AssetDatabase.LoadAssetAtPath<Environment>("Assets/Askowl/Decoupler/Scripts/Environments/Mock.asset");
        assets.Add(
                 ("ServicesManager", $"Decoupler.Services.{assets.destinationName}ServicesManager")
               , ("ContextAsset", $"Decoupler.Services.{assets.destinationName}Context")
               , ("ServiceForMock", $"Decoupler.Services.{assets.destinationName}ServiceForMock")
               , ("Wizard", $"Decoupler.Services.{assets.destinationName}Wizard"))
              .SetField("ServicesManager", "context", "ContextAsset")
              .SetField("ServiceForMock",  "context", "ContextAsset")
              .InsertIntoArrayField("ServicesManager", "services", "ServiceForMock")
              .SetField("ContextAsset", "environment", mockEnvironment);
        Debug.Log("...All Done");
      }
    }
  }
}