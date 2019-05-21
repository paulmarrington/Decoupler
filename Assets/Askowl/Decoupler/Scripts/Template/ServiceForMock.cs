using Askowl;
using UnityEngine;

namespace Decoupler.Services {
  /*--[CreateAssetMenu(menuName = "Decoupled/_Template_/ServiceForMock", fileName = "_Template_ServiceForMock")]--*/
  public class _Template_ServiceForMock : _Template_ServiceAdapter {
    /*-EntryPoint...-*/
    public override Emitter Call(EntryPoint EntryPoint) {
      Debug.Log($"*** Mock Call '{GetType().Name}' 'EntryPoint', '{EntryPoint.request}");
      return null;
    }
    /*-...EntryPoint-*/

    public override bool IsExternalServiceAvailable() => true;
  }
}