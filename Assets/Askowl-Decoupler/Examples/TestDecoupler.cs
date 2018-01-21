using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestDecoupler {
  [UnityTest]
  public IEnumerator DefaultServiceTest() {
    Decoupled.TestDecouplerInterface.Reset();

    Decoupled.TestDecouplerInterface testDecoupler = Decoupled.TestDecouplerInterface.Instance;

    testDecoupler.entry1(12);
    Assert.AreEqual(testDecoupler.entry2(), 12);
    yield return null;
  }

  [UnityTest]
  public IEnumerator SingletonAssetTest() {
    Decoupled.TestDecouplerInterface.Reset();

    yield return Decoupled.TestDecouplerInterface.Register();

    Decoupled.TestDecouplerInterface testDecoupler = Decoupled.TestDecouplerInterface.Instance;

    testDecoupler.entry1(12);
    Assert.AreEqual(testDecoupler.entry2(), 24);
  }
}

namespace Decoupled {
  public class TestDecouplerInterface : Decoupled.Service<TestDecouplerInterface> {
    protected int number = 0;

    public virtual void entry1(int number) {
      this.number = number;
    }

    public virtual int entry2() {
      return number;
    }
  }
}

public class TestDecouplerService : Decoupled.TestDecouplerInterface {

  public override void entry1(int number) {
    this.number = number * 2;
  }

  public override int entry2() {
    return number * 2;
  }
}
