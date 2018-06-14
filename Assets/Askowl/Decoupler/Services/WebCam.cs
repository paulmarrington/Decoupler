using System;
using UnityEngine;

namespace Decoupled {
  /// <inheritdoc />
  /// <summary>
  /// Decoupled interface to the device camera.
  /// </summary>
  public class WebCam : Service<WebCam> {
    /// <summary>
    /// Configuration data for the webcam - set by MonoBehaviour, CustomAsset or ScriptableObject
    /// </summary>
    [Serializable]
    public class Setup {
      [SerializeField] internal bool UseFrontFacing = false;
      [SerializeField] internal bool IsFullScreen   = true;
    }

    private Setup setup;

    /// <summary>
    /// Used to access a decoupled instance of the service - or a default one if none are registered
    /// </summary>
    /// <param name="webCamSetup">Serialisable data to set in MonoBehaviour or CustomAsset/ScriptableObject</param>
    public static WebCam Instance(Setup webCamSetup) {
      var webCam = Service<WebCam>.Instance;
      webCam.setup = webCamSetup;
      return webCam;
    }

    /// <summary>
    /// Set in Unity inspector. Only applicable on devices with opposing cameras
    /// </summary>
    public bool UseFrontFacing { get { return setup.UseFrontFacing; } }

    /// <summary>
    /// Set in Unity inspector. If not true the application will need to set Width and Height
    /// </summary>
    public bool IsFullScreen { get { return setup.IsFullScreen; } }

    /// <summary>
    /// True if the camera is on and available. Can be used to turn camera on and off
    /// </summary>
    public virtual bool Playing { get; set; }

    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Get or set the frames per second. Hardware may or may not take notice of a set
    /// </summary>
    public virtual float FPS { get; set; }

    /// <summary>
    /// Set/get the width of the display in pixels
    /// </summary>
    public virtual int Width { get; set; }

    /// <summary>
    /// Set/get the height of the display in pixels
    /// </summary>
    public virtual int Height { get; set; }

    /// <summary>
    /// Returns an clockwise angle (in degrees), which can be used to rotate a polygon so camera contents are shown in correct orientation.
    /// </summary>
    public virtual int RotationAngle { get { return 0; } }

    /// <summary>
    /// Returns true if the texture image is vertically flipped.
    /// </summary>
    public virtual int VerticalMirror { get { return 1; } }

    /// <summary>
    /// Did the video buffer update this frame?
    /// </summary>
    public virtual bool DidUpdateThisFrame { get { return false; } }

    /// <summary>
    /// Used by a AspectRatioFitter to make the image look correct
    /// </summary>
    public float AspectRatio { get { return (float) Width / Height; } }

    /// <summary>
    /// Stops the camera
    /// </summary>
    public virtual void Stop() { }
  }
}