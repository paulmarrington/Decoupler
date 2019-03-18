﻿// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages
#if AskowlTests
using System.Collections;
using System.IO;
using Askowl.Gherkin;
using CustomAsset;
using CustomAsset.Mutable;
using Decoupler.Services;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;
// ReSharper disable MissingXmlDoc

namespace Askowl.Decoupler.Examples {
  public class CreateServiceExamples : PlayModeTests {
    private string      projectDirectory;
    private AssetDb     assetDb;
    private AssetEditor assetEditor;

    private IEnumerator ServiceTest(string label) {
      yield return Feature.Go("DecouplerDefinitions", featureFile: "CreateServices", label).AsCoroutine();
      if (projectDirectory != null) AssetDb.Instance.Delete(projectDirectory);
      assetDb?.Dispose();
      assetEditor.Dispose();
    }

    [UnityTest] public IEnumerator CreateEmptyService() { yield return ServiceTest("@CreateEmptyService"); }

    [Step(@"^we prepare for a new service$")] public void SetProjectDirectory() {
      assetDb?.Dispose();
      assetDb          = AssetDb.Instance;
      projectDirectory = $"/Assets/Temp/{GUID.Generate()}";
      assetDb.CreateFolders(path: projectDirectory).Select().Dispose();
      if (assetDb.Error) Fail($"Can't create '{projectDirectory}'");

      assetEditor?.Dispose();
      assetEditor = AssetEditor.Instance("CreateServiceExamples", "Assets/Askowl/Decoupler/Scripts/Editor")
                               .Load(("NewService", "Decoupler.NewService"));
    }
    [Step(@"^we set ""(.*?)"" to ""(.*?)""$")] public void SetField(string[] matches) =>
      assetEditor.SetField(assetName: "NewService", fieldName: matches[0], fieldValue: matches[1]);

    [Step(@"^we create the new service$")] public void CreateService() =>
      ((AssetWizard) assetEditor.Asset("NewService")).Create();

    [Step(@"^processing is complete$")] public Emitter ProcessingComplete() =>
      AssetEditor.onCompleteEmitter.Listen(validation: ("AssetEditor", "CreateServiceExamples"), once: true);

    [Step(@"^there are no errors in the log$")] public void NoErrors() { }

    /*
    [Step(@"^$")] public void () { }
    */
  }
}
#endif