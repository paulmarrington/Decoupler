// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages
#if AskowlTests
using System.Collections;
using System.IO;
using Askowl.Gherkin;
using CustomAsset;
using CustomAsset.Mutable;
using Decoupler.Services;
using UnityEditor;
using UnityEngine.Assertions;
using UnityEngine.TestTools;
// ReSharper disable MissingXmlDoc

namespace Askowl.Decoupler.Examples {
  public class CreateServiceExamples : PlayModeTests {
    private string path;

    private IEnumerator ServiceTest(string label) {
      yield return Feature.Go("DecouplerDefinitions", featureFile: "CreateServices", label).AsCoroutine();
    }

    [UnityTest] public IEnumerator CreateEmptyService() { yield return ServiceTest("@CreateEmptyService"); }

    [Step(@"^we are in the project directory ""(.*)""$")] public void ServerStack(string[] matches) {
      if (!Directory.Exists(path = matches[0])) {
        AssetDb.CreateFolders(path);
        AssetDb.SelectFolder(path);
      }
    }

    /*
    [Step(@"^$")] public void () { }
    */
  }
}
#endif