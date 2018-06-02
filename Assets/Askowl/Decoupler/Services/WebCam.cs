using JetBrains.Annotations;
using UnityEngine;

namespace Decoupled {
  public class WebCam : Service<WebCam> {
    [SerializeField] private bool useFrontFacing;
    [SerializeField] private bool isFullScreen;

    
    public virtual bool Playing { get { return false; } set { } }

    
    public virtual float FPS { get { return 0; } set { } }

    
    public virtual int Width { get { return 0; } set { } }

    
    public virtual int Height { get { return 0; } set { } }

    
    public virtual int RotationAngle { get { return 0; } }

    
    public virtual int VerticalMirror { get { return 1; } }

    
    public virtual bool DidUpdateThisFrame { get { return false; } }

    
    public virtual float AspectRatio { get { return 1; } }

    public virtual void Stop() { }
  }
}