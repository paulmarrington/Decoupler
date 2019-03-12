// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using Askowl;
using UnityEngine;

namespace Decoupler.Services {
  /// <a href=""></a> //#TBD#//
  [CreateAssetMenu(menuName = "Decoupled/_Template_/ServiceForMock", fileName = "_Template_ServiceForMock")]
  public class _Template_ServiceForMock : _Template_ServiceAdapter {
    /*-EntryPoint...-*/
    /// <inheritdoc />
    public override Emitter Call(Service<EntryPointDto> service) {
      Debug.Log($"*** Call '{GetType().Name}' '{typeof(EntryPointDto).Name}'"); //#DM#//
      return null;
    }
    /*-EntryPoint...-*/

    /// <inheritdoc />
    public override bool IsExternalServiceAvailable() => true;
  }
}