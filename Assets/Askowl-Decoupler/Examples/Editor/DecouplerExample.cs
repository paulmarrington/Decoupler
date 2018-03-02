using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public sealed class TestDecoupler {
  [Test]
  public void DefaultServiceTest() {
    Decoupled.TestDecouplerInterface.Reset();

    Decoupled.TestDecouplerInterface testDecoupler = Decoupled.TestDecouplerInterface.Instance;

    testDecoupler.Entry1(number: 12);
    Assert.AreEqual(expected: testDecoupler.Entry2(), actual: 12);
  }

  [Test]
  public void LoadImplementedServiceTest() {
    Decoupled.TestDecouplerInterface.Reset();

    Decoupled.TestDecouplerInterface created =
      Decoupled.TestDecouplerInterface.Load<TestDecouplerService>();

    Decoupled.TestDecouplerInterface testDecoupler = Decoupled.TestDecouplerInterface.Instance;

    Assert.AreEqual(expected: testDecoupler, actual: created);

    testDecoupler.Entry1(number: 12);
    Assert.AreEqual(expected: 24, actual: testDecoupler.Entry2());
  }

  [UnityTest]
  public IEnumerator ControllerImplementedServiceTest() {
    Decoupled.TestDecouplerInterface.Reset();

    yield return TestDecouplerService.Register<TestDecouplerService>();

    Decoupled.TestDecouplerInterface testDecoupler = Decoupled.TestDecouplerInterface.Instance;

    testDecoupler.Entry1(number: 12);
    Assert.AreEqual(expected: 24, actual: testDecoupler.Entry2());
  }
}

namespace Decoupled {
  public class TestDecouplerInterface : Service<TestDecouplerInterface> {
    protected int Number;

    internal virtual void Entry1(int number) { Number = number; }

    internal int Entry2() { return Number; }
  }
}

internal sealed class TestDecouplerService : Decoupled.TestDecouplerInterface {
  internal override void Entry1(int number) { Number = number * 2; }
}