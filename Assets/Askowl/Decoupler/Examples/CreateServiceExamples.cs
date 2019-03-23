// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages
#if AskowlTests
using System;
using System.Collections;
using System.IO;
using Askowl.Gherkin;
using CustomAsset;
using Decoupler;
using Decoupler.Services;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;
using String = CustomAsset.Mutable.String;
// ReSharper disable MissingXmlDoc

namespace Askowl.Decoupler.Examples {
  public class CreateServiceExamples : PlayModeTests {
    private string      projectDirectory;
    private AssetDb     assetDb;
    private AssetEditor assetEditor;
    private string      fileBase;
    private NewService  newService;

    private IEnumerator ServiceTest(string label) {
      yield return Feature.Go("DecouplerDefinitions", featureFile: "CreateServices", label).AsCoroutine();
      assetDb?.Dispose();
      assetEditor?.Dispose();
    }

    [UnityTest] public IEnumerator CreateEmptyService() { yield return ServiceTest("@CreateEmptyService"); }
    [UnityTest] public IEnumerator WithContext()        { yield return ServiceTest("@CreateServiceWithContext"); }
    [UnityTest] public IEnumerator WithEntryPoints()    { yield return ServiceTest("@CreateServiceWithEntryPoints"); }
    [UnityTest] public IEnumerator ConcreteService()    { yield return ServiceTest("@CreateConcreteService"); }

    [Step(@"^we prepare for a new service$")] public void PrepareNewService() {
      assetDb?.Dispose();
      assetDb          = AssetDb.Instance;
      projectDirectory = $"/Assets/Temp/CreateServiceExamples";
      assetDb.CreateFolders(path: projectDirectory).Select();
      if (assetDb.Error) Fail($"Can't create '{projectDirectory}'");

      assetEditor?.Dispose();
      assetEditor = AssetEditor.Instance("CreateServiceExamples", projectDirectory.Substring(1))
                               .Load((name: "NewService", asset: "Decoupler.NewService"))
                               .SetField("NewService", "destinationPath", fieldValue: projectDirectory);
      newService = (NewService) assetEditor.Asset("NewService");
    }

    [Step(@"^we set ""(.*?)"" to ""(.*?)""$")] public void SetField(string[] matches) {
      fileBase = $"{matches[1]}_{(int) Clock.EpochTimeNow}";
      assetEditor.SetField(assetName: "NewService", fieldName: matches[0], fieldValue: fileBase);
    }

    [Step(@"^we create the new service$")] public void CreateService() =>
      ((AssetWizard) assetEditor.Save().Asset("NewService")).Create();

    [Step(@"^we add entry points:$")] public void AddEntryPoints(string[][] table) {
      for (int row = 1; row < table.Length; row++) {
        newService.AddEntryPoint(table[row][0], table[row][1], table[row][2]);
      }
    }

    // Not much we can do from here on in because this thread appears to be terminated by the compile.
    [Step(@"^processing is complete$")] public void ProcessingComplete() { }

    [Step(@"^there are no errors in the log$")] public void NoErrors() { }

    /*
    [Step(@"^$")] public void () { }
    */
  }
}
#endif