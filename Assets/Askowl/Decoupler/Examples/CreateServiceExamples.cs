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

    private IEnumerator ServiceTest(string label) {
      yield return Feature.Go("DecouplerDefinitions", featureFile: "CreateServices", label).AsCoroutine();
      assetDb?.Dispose();
      assetEditor?.Dispose();
    }

    [UnityTest] public IEnumerator CreateEmptyService() { yield return ServiceTest("@CreateEmptyService"); }

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
    }

    [Step(@"^we set ""(.*?)"" to ""(.*?)""$")] public void SetField(string[] matches) {
      fileBase = $"{matches[1]}_{(int) Clock.EpochTimeNow}";
      assetEditor.SetField(assetName: "NewService", fieldName: matches[0], fieldValue: fileBase);
    }

    [Step(@"^we create the new service$")] public void CreateService() =>
      ((AssetWizard) assetEditor.Save().Asset("NewService")).Create();

    [Step(@"^processing is complete$")] public Emitter ProcessingComplete() {
      var wizard = $"{projectDirectory}/{fileBase}/{fileBase} Wizard.asset";
      return Fiber.Start.Begin.WaitFor(0.2f).Until(_ => File.Exists(wizard)).OnComplete;
    }

    [Step(@"^there are no errors in the log$")] public void NoErrors() { }

    /*
    [Step(@"^$")] public void () { }
    */
  }
}
#endif