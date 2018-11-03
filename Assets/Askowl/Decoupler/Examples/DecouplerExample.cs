#if UNITY_EDITOR && AskowlDecoupler
using Decoupled;
using NUnit.Framework;

/// <a href="">Unit tests for Decoupler system</a> //#TBD#//
public sealed class TestDecoupler {
  /// <a href="">Make sure the default works if no concrete services are registered</a> //#TBD#//
  [Test] public void DefaultServiceTest() {
    FirstDecouplerInterface.Reset();

    var firstDecoupler = FirstDecouplerInterface.Instance;

    firstDecoupler.Entry1(number: 12);
    Assert.AreEqual(expected: firstDecoupler.Entry2(), actual: 12);
  }

  /// <a href="">Make sure no calls slip through to the default service after registration</a> //#TBD#//
  [Test] public void LoadImplementedServiceTest() {
    FirstDecouplerInterface.Reset();

    FirstDecouplerInterface.Register<FirstDecouplerService>();

    var firstDecoupler = FirstDecouplerInterface.Instance;

    firstDecoupler.Entry1(number: 12);
    Assert.AreEqual(expected: 24, actual: firstDecoupler.Entry2());
  }
}
#endif