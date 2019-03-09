// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Askowl;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Decoupler {
  /// <a href=""></a> //#TBD#//
  public class NewService : AssetWizard {
    private static NewService newServiceInputForm;

    [MenuItem("Assets/Create/Decoupled/New Service")] private static void Start() {
      if (newServiceInputForm == null) newServiceInputForm = CreateInstance<NewService>();
      Selection.activeObject = newServiceInputForm;
    }

    [SerializeField] private string newServiceName;
    [SerializeField, Tooltip("C# definitions, as in 'String str;RuntimePlatform platform'"), Multiline]
    private string context = "RuntimePlatform platform;";
    [SerializeField] private ServiceMeta[] entryPoints;

    /// <a href=""></a> //#TBD#//
    [Serializable] public struct ServiceMeta {
      [SerializeField]            private string entryPointName;
      [SerializeField, Multiline] private string requestData;
      [SerializeField, Multiline] private string responseData;
    }
    private string destinationPath;

    internal void Clear() {
      newServiceName = context = "";
      entryPoints    = default;
    }

    internal void Create() {
      StringBuilder builder = new StringBuilder();
      for (int i = 0; i < entryPoints.Length; i++) {
        ParseEntryPoint(builder, entryPoints[i]);
      }
      CreateAssets(
        "Decoupler",
        "Template",            newServiceName,
        "/*-ContextFields-*/", ParseFields(context), "/*-ContextEquality-*/", ContextEquality(context),
        "/*-EntryPoints-*/",   builder.ToString());
    }

    private string ParseCS(string cs, Func<string, string, string> toString) {
      StringBuilder builder = new StringBuilder();
      var           pairs   = csRegex.Split(cs);
      for (int i = 0; i < pairs.Length; i += 2) builder.Append(toString(pairs[i], pairs[i + 1]));
      return builder.ToString();
    }
    static Regex csRegex = new Regex(@"\s*;\s*|\s*,\s*|\s+", RegexOptions.Singleline);

    private string ParseFields(string cs) =>
      ParseCS(cs, (type, fieldName) => $@"    [SerializedField] public {type} {fieldName};\n");

    private string ContextEquality(string cs) =>
      ParseCS(cs, (type, fieldName) => $@" && Equals({fieldName}, other.{fieldName})");

    private string ParseEntryPoint(StringBuilder builder, ServiceMeta entryPoint) { }

    /// <a href=""></a> //#TBD#//
    protected override string GetDestinationPath() => $"{GetSelectedPathInProjectView()}/{newServiceName}";

    private static string GetSelectedPathInProjectView() {
      string path = "Assets";
      foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets)) {
        path = AssetDatabase.GetAssetPath(obj);
        if (!string.IsNullOrEmpty(path) && File.Exists(path)) {
          path = Path.GetDirectoryName(path);
          break;
        }
      }
      return path;
    }
    protected override void OnScriptReload() { }
  }

  [CustomEditor(typeof(NewService))] internal class NewServiceEditor : Editor {
    public override void OnInspectorGUI() {
      if (GUILayout.Button("Clear")) {
        ((NewService) target).Clear();
        GUI.FocusControl(null);
      }
      serializedObject.Update();
      DrawDefaultInspector();
      if (GUILayout.Button("Create")) ((NewService) target).Create();
      serializedObject.ApplyModifiedProperties();
    }
  }
}