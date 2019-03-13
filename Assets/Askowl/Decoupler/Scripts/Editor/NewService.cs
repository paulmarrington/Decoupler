// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using System.Text.RegularExpressions;
using Askowl;
using CustomAsset;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using GameObject = UnityEngine.GameObject;

namespace Decoupler {
  /// <a href=""></a> //#TBD#//
  public class NewService : AssetWizard {

    [MenuItem("Assets/Create/Decoupled/New Service")] private static void Start() {
      var newServiceInputForm = Wizard;
      EditorUtility.FocusProjectWindow();
      Selection.activeObject = newServiceInputForm;
      EditorGUI.FocusTextInControl("FirstWizardField");
    }

    private static NewService Wizard {
      get {
        if (wizard != default) return wizard;
        return wizard = LoadOrCreate<NewService>("Assets/Askowl/Decoupler/Scripts/Editor/NewService.asset");
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
    }

    protected override void Create() => CreateAssets("Decoupler");

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

    protected NewService OnScriptReload() {
      Environment mockEnvironment =
        AssetDatabase.LoadAssetAtPath<Environment>("Assets/Askowl/Decoupler/Scripts/Environments/Mock.asset");

      CreateAssetDictionary(
        ("servicesManager", ScriptableType($"Decoupler.Services.{destinationName}ServicesManager"))
      , ("contextAsset", ScriptableType($"Decoupler.Services.{destinationName}Context"))
      , ("serviceForMock", ScriptableType($"Decoupler.Services.{destinationName}ServiceForMock"))
      , ("wizard", ScriptableType($"Decoupler.Services.{destinationName}Wizard")));

      SetField("servicesManager", "context", Asset("contextAsset"));
      SetField("servicesManager", "context", Asset("contextAsset"));
      SetField("serviceForMock",  "context", Asset("contextAsset"));
      InsertIntoArrayField("servicesManager", "services", Asset("contextAsset"));
      SetField("contextAsset", "environment", mockEnvironment);
      return this;
    }

    [DidReloadScripts] private static void Phase2() {
      Wizard.OnScriptReload().SaveAssetDictionary();
      Debug.Log("...All Done");
    }
  }
}