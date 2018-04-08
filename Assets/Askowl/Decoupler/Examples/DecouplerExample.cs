using Decoupled;
using NUnit.Framework;

public sealed class TestDecoupler {
  [Test]
  public void DefaultServiceTest() {
    FirstDecouplerInterface.Reset();

    FirstDecouplerInterface firstDecoupler = FirstDecouplerInterface.Instance;

    firstDecoupler.Entry1(number: 12);
    Assert.AreEqual(expected: firstDecoupler.Entry2(), actual: 12);
  }

  [Test]
  public void LoadImplementedServiceTest() {
    FirstDecouplerInterface.Reset();

    FirstDecouplerInterface created =
      FirstDecouplerInterface.Register<FirstDecouplerService>();

    FirstDecouplerInterface firstDecoupler = FirstDecouplerInterface.Instance;

    Assert.AreEqual(expected: firstDecoupler, actual: created);

    firstDecoupler.Entry1(number: 12);
    Assert.AreEqual(expected: 24, actual: firstDecoupler.Entry2());
  }
}