// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using Askowl;
using UnityEngine;

namespace Decoupler.Services {
  /*--[CreateAssetMenu(menuName = "Decoupled/_Template_/ServiceForMock", fileName = "_Template_ServiceForMock")]--*/
  public class _Template_ServiceForMock : _Template_ServiceAdapter {
    /*-EntryPoint...-*/
    /// <inheritdoc />
    public override Emitter Call(Service<EntryPointDto> service) {
      Debug.Log($"*** Mock Call '{GetType().Name}' '{typeof(EntryPointDto).Name}', '{service.Dto.request}");
      return null;
    }
    /*-...EntryPoint-*/

    public override bool IsExternalServiceAvailable() => true;
  }
}