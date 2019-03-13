// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using System.Text.RegularExpressions;
using Askowl;
using UnityEditor;
using UnityEngine;

namespace Decoupler {
  /// <a href=""></a> //#TBD#//
  public class NewService : AssetWizard {
    private static NewService newServiceInputForm;

    [MenuItem("Assets/Create/Decoupled/New Service")] private static void Start() {
      if (newServiceInputForm == null) newServiceInputForm = CreateInstance<NewService>();
      Selection.activeObject = newServiceInputForm;
      EditorGUI.FocusTextInControl("FirstWizardField");
    }

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
    private string destinationPath;

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

    protected override void OnScriptReload() { }
  }
}