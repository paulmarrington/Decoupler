// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using System.Text.RegularExpressions;
using Askowl;
using UnityEditor;
using UnityEngine;

namespace Decoupler {
  /// <a href="http://bit.ly/2OZP7gP">New Service Wizard</a>
  public class NewService : AssetWizard {
    [MenuItem("Assets/Create/Decoupled/New Service")] private static void Start() {
      wizard.Value.basePath  = AssetDb.ProjectFolder();
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

    /// <a href="http://bit.ly/2OZP7gP">Set the service base path. Leave null to be editor project current path. Used in testing</a>
    public NewService BasePath(string path) {
      basePath = path;
      return this;
    }
    private string basePath = null;

    /// <a href="http://bit.ly/2OZP7gP">Add an entry point manually instead of using inspector. Used in testing</a>
    public void AddEntryPoint(string entryPointName, string request, string response) {
      var meta = new ServiceMeta[entryPoints.Length + 1];
      entryPoints.CopyTo(meta, 0);
      meta[entryPoints.Length] = new ServiceMeta
        {entryPointName = entryPointName, requestData = request, responseData = response};
      entryPoints = meta;
    }

    public override void Clear() {
      newServiceName = context = "";
      entryPoints    = new ServiceMeta[0];
    }

    public override void Create() {
      Label = "BuildNewService";
      CreateAssets(assetName: newServiceName, assetType: "Decoupler", basePath);
      BuildAssets.Display();
    }
    protected override string TemplatePath() => "Assets/Askowl/Decoupler/Scripts/Template";

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
                inner.And(re(@"int /*-entryPointRequest-*/\s*"), ToTuple(entryPoints[i].requestData) ?? "string ");
              if (!string.IsNullOrWhiteSpace(entryPoints[i].responseData))
                inner.And(re(@"int /*-entryPointResponse-*/\s*"), ToTuple(entryPoints[i].responseData) ?? "string ");
              inner.Add();
            }
      }

      return template.Substitute("_Template_", newServiceName)
                     .And(re("/*-destination-*/"), destination)
                     .And(re("/*-assetType-*/"),   type)
                     .And(re("/*--"),              "")
                     .And(re("--*/"),              "")
                     .Result();
    }
  }
}