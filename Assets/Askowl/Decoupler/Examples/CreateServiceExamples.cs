// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages
#if !ExcludeAskowlTests
using System;
using System.Collections;
using System.IO;
using Askowl;
using Askowl.Gherkin;
using Decoupler;
using NUnit.Framework;
using UnityEngine.TestTools;
using Random = System.Random;
// ReSharper disable MissingXmlDoc

namespace Decoupler.Examples {
  public class CreateServiceExamples : PlayModeTests {
    private        string      projectDirectory = "Assets/Temp/CreateServiceExamples";
    private        AssetDb     assetDb;
    private        AssetEditor assetEditor;
    private        NewService  newService;
    private static Emitter     afterCompile;
    private        string      serviceDirectory, serviceName, concreteWizard;

    private IEnumerator ServiceTest(string label) {
      Clear();
      yield return Feature.Go("DecouplerDefinitions", featureFile: "CreateServices", label).AsCoroutine();
      assetDb?.Dispose();
      assetEditor?.Dispose();
    }
    private void Clear() {
      if (newService == null) return;
      newService.Clear();
      assetEditor.SerialisedAsset("NewService").Update();
    }

    [UnityTest, Timeout(180000)] public IEnumerator EmptyService() { yield return ServiceTest("@CreateEmptyService"); }
    [UnityTest, Timeout(180000)] public IEnumerator WithContext() {
      yield return ServiceTest("@CreateServiceWithContext");
    }
    [UnityTest, Timeout(180000)] public IEnumerator WithEntryPoints() {
      yield return ServiceTest("@CreateServiceWithEntryPoints");
    }
    [UnityTest, Timeout(180000)] public IEnumerator ZAddConcreteService() {
      yield return ServiceTest("@AddConcreteService");
    }

    [Step(@"^we prepare for a new service$")] public void PrepareNewService() {
      assetDb?.Dispose();
      assetDb = AssetDb.Instance;
      assetDb.CreateFolders(path: projectDirectory).Select();
      if (assetDb.Error) Fail($"Can't create '{projectDirectory}'");

      assetEditor?.Dispose();
      assetEditor = AssetEditor.Instance.Load(assetName: "NewService", nameSpace: "Decoupler", projectDirectory);
      newService  = (NewService) assetEditor.Asset("NewService");
      Clear();
    }
    [Step(@"^we set ""(.*?)"" to ""(.*?)""$")] public void SetField(string[] matches) =>
      assetEditor.SetField(assetName: "NewService", fieldName: matches[0], fieldValue: matches[1]);
    [Step(@"^we set ""(.*?)"" to unique ""(.*?)""$")] public void SetFieldUnique(string[] matches) =>
      assetEditor.SetField("NewService", "newServiceName", $"{matches[1]}_{(int) Clock.EpochTimeNow}");
    [Step(@"^we create the new service$")] public void CreateService() =>
      ((NewService) assetEditor.Save().Asset("NewService")).BasePath(projectDirectory).Create();
    [Step(@"^we add entry points:$")] public void AddEntryPoints(string[][] table) {
      assetEditor.SerialisedAsset("NewService").ApplyModifiedPropertiesWithoutUndo();
      for (int row = 1; row < table.Length; row++)
        newService.AddEntryPoint(table[row][0], table[row][1], table[row][2]);
      assetEditor.SerialisedAsset("NewService").Update();
    }
    [Step(@"^a service we created earlier$")] public void FindService(Definitions definitions) {
      try {
        var directories = Directory.GetDirectories(projectDirectory);
        if (directories.Length == 0) throw new Exception();
        serviceDirectory = directories[(new Random()).Next(0, directories.Length - 1)];
        serviceName      = Path.GetFileName(serviceDirectory);
      } catch {
        definitions.Error("Another decoupled service test must be completed before we can add a concrete service");
      }
    }
    [Step(@"^we open the concrete service wizard$")] public void OpenWizard() {
      PrepareNewService();
      assetEditor.Load(concreteWizard = $"{serviceName}Wizard", "Decoupler.Services", serviceDirectory);
    }
    [Step(@"^give the concrete service a name$")] public void ConcreteServiceName() =>
      assetEditor.SetField(concreteWizard, $"new{serviceName}Name", $"Concrete_{(int) Clock.EpochTimeNow}");
    [Step(@"^we create the new concrete service$")] public void CreateConcreteService() {
      // we can't actually run this as part of the stream as assets haven't been built yet
      // ((ICreate) assetEditor.Save().Asset(concreteWizard)).Create();
    }
  }
}
#endif