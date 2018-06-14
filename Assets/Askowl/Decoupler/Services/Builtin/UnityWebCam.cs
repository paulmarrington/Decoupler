using UnityEngine;

namespace Askowl.Mars {
  /// <inheritdoc />
  public class UnityWebCam : Decoupled.WebCam {
    private readonly WebCamTexture webCamTexture;

    /// <inheritdoc />
    public UnityWebCam() {
      for (int i = 0; i < WebCamTexture.devices.Length; i++) {
        if (WebCamTexture.devices[i].isFrontFacing == UseFrontFacing) {
          webCamTexture = new WebCamTexture(WebCamTexture.devices[i].name);
          if (webCamTexture != null) break;
        }
      }

      if (IsFullScreen) {
        Width  = Screen.width;
        Height = Screen.height;
      }
    }

    /// <inheritdoc />
    public override bool Playing {
      get { return (webCamTexture.isPlaying); }
      set {
        if (value) {
          webCamTexture.Play();
        } else {
          webCamTexture.Pause();
        }
      }
    }

    /// <inheritdoc />
    public override float FPS {
      get { return webCamTexture.requestedFPS; }
      set { webCamTexture.requestedFPS = value; }
    }

    /// <inheritdoc />
    public sealed override int Width {
      get { return webCamTexture.width; }
      set { webCamTexture.requestedWidth = value; }
    }

    /// <inheritdoc />
    public sealed override int Height {
      get { return webCamTexture.height; }
      set { webCamTexture.requestedHeight = value; }
    }

    /// <inheritdoc />
    public override int RotationAngle { get { return webCamTexture.videoRotationAngle; } }

    /// <inheritdoc />
    public override int VerticalMirror {
      get { return webCamTexture.videoVerticallyMirrored ? -1 : 1; }
    }

    /// <inheritdoc />
    public override bool DidUpdateThisFrame { get { return webCamTexture.didUpdateThisFrame; } }

    /// <inheritdoc />
    public override void Stop() { webCamTexture.Stop(); }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void RegisterService() {
      if (WebCamTexture.devices.Length > 0) Register<UnityWebCam>();
    }
  }
}