﻿// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using Askowl;
using UnityEngine;

namespace CustomAsset.Services {
  /// <a href=""></a> //#TBD#//
  [CreateAssetMenu(menuName = "Custom Assets/Services/Template/ServiceForMock", fileName = "TemplateServiceForMock")]
  public class TemplateServiceForMock : TemplateServiceAdapter {
    /// <a href="">Prepare the mock service for operations</a> //#TBD#//
    protected override void Prepare() => base.Prepare();

    /// <a href="">Use Log and Error to record analytics based on service responses</a> //#TBD#//
    protected override void LogOnResponse(Emitter emitter) => base.LogOnResponse(emitter);

    public override Emitter Call(Service<TemplateServiceDto> service) => throw new NotImplementedException();

    /// <inheritdoc />
    public override bool IsExternalServiceAvailable() => true;
  }
}