#if UNITY_EDITOR
using Decoupled;
using NUnit.Framework;

/// <summary>
/// Unit tests for Decoupler system
/// </summary>
public sealed class TestDecoupler {
  /// <summary>
  /// Make sure the default works if no concrete services are registered
  /// </summary>
  [Test]
  public void DefaultServiceTest() {
    FirstDecouplerInterface.Reset();

    var firstDecoupler = FirstDecouplerInterface.Instance;

    firstDecoupler.Entry1(number: 12);
    Assert.AreEqual(expected: firstDecoupler.Entry2(), actual: 12);
  }

  /// <summary>
  /// Make sure no calls slip through to the default service after registration
  /// </summary>
  [Test]
  public void LoadImplementedServiceTest() {
    FirstDecouplerInterface.Reset();

    FirstDecouplerInterface.Register<FirstDecouplerService>();

    var firstDecoupler = FirstDecouplerInterface.Instance;

    firstDecoupler.Entry1(number: 12);
    Assert.AreEqual(expected: 24, actual: firstDecoupler.Entry2());
  }
}
#endif