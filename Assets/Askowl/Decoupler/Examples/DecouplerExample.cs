using Decoupled;
using NUnit.Framework;

public sealed class TestDecoupler {
  [Test]
  public void DefaultServiceTest() {
    TestDecouplerInterface.Reset();

    TestDecouplerInterface testDecoupler = TestDecouplerInterface.Instance;

    testDecoupler.Entry1(number: 12);
    Assert.AreEqual(expected: testDecoupler.Entry2(), actual: 12);
  }

  [Test]
  public void LoadImplementedServiceTest() {
    TestDecouplerInterface.Reset();

    TestDecouplerInterface created =
      TestDecouplerInterface.Register<TestDecouplerService>();

    TestDecouplerInterface testDecoupler = TestDecouplerInterface.Instance;

    Assert.AreEqual(expected: testDecoupler, actual: created);

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

internal sealed class TestDecouplerService : TestDecouplerInterface {
  internal override void Entry1(int number) { Number = number * 2; }
}