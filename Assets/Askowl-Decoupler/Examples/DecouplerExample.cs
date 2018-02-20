using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class TestDecoupler {
  [Test]
  public void DefaultServiceTest() {
    Decoupled.TestDecouplerInterface.Reset();

    Decoupled.TestDecouplerInterface testDecoupler = Decoupled.Service.Instance<Decoupled.TestDecouplerInterface>();

    testDecoupler.entry1(12);
    Assert.AreEqual(testDecoupler.entry2(), 12);
  }

  [Test]
  public void LoadImplementedServiceTest() {
    Decoupled.TestDecouplerInterface.Reset();
  
    Decoupled.TestDecouplerInterface created = Decoupled.TestDecouplerInterface.Load<TestDecouplerService>();
  
    Decoupled.TestDecouplerInterface testDecoupler = Decoupled.Service.Instance<Decoupled.TestDecouplerInterface>();
  
    Assert.AreEqual(testDecoupler, created);
  
    testDecoupler.entry1(12);
    Assert.AreEqual(24, testDecoupler.entry2());
  }
  
  [UnityTest]
  public IEnumerator ControllerImplementedServiceTest() {
    Decoupled.TestDecouplerInterface.Reset();
  
    yield return TestDecouplerService.Register<TestDecouplerService>();
  
    Decoupled.TestDecouplerInterface testDecoupler = Decoupled.Service.Instance<Decoupled.TestDecouplerInterface>();
  
    testDecoupler.entry1(12);
    Assert.AreEqual(24, testDecoupler.entry2());
  }
}

namespace Decoupled {
  public class TestDecouplerInterface : Service {
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
}
