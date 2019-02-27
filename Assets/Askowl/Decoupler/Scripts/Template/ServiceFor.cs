﻿// Copyright 2019 (C) paul@marrington.net http://www.askowl.net/unity-packages

using UnityEngine;
#if TemplateServiceFor || true
using Askowl;
// Add using statements for service library here
#endif

namespace CustomAsset.Services {
  /// <a href=""></a><inheritdoc /> //#TBD#//
  [CreateAssetMenu(menuName = "Custom Assets/Services/Template/Service", fileName = "TemplateServiceFor")]
  public abstract class TemplateServiceFor : TemplateServiceAdapter {
    #if TemplateServiceFor || true
    protected override void Prepare() => base.Prepare();

    protected override void LogOnResponse(Emitter emitter) => base.LogOnResponse(emitter);

    // Implement all interface methods that call concrete service adapters need to implement

    // One service override per service method
    // Access the external service here. Save and call dti.Emitter.Fire when service call completes
    // or set dto.ErrorMessage if the service call fails to initialise

    #endif
  }
}