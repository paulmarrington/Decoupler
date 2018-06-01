using JetBrains.Annotations;
using UnityEngine;

namespace Decoupled {
  public class WebCam : Service<WebCam> {
    [SerializeField] private bool useFrontFacing;
    [SerializeField] private bool isFullScreen;

    [UsedImplicitly]
    public virtual bool Playing { get { return false; } set { } }

    [UsedImplicitly]
    public virtual float FPS { get { return 0; } set { } }

    [UsedImplicitly]
    public virtual int Width { get { return 0; } set { } }

    [UsedImplicitly]
    public virtual int Height { get { return 0; } set { } }

    [UsedImplicitly]
    public virtual int RotationAngle { get { return 0; } }

    [UsedImplicitly]
    public virtual int VerticalMirror { get { return 1; } }

    [UsedImplicitly]
    public virtual bool DidUpdateThisFrame { get { return false; } }

    [UsedImplicitly]
    public virtual float AspectRatio { get { return 1; } }

    public virtual void Stop() { }
  }
}