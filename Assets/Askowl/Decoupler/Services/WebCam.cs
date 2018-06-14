﻿using UnityEngine;

namespace Decoupled {
  /// <inheritdoc />
  /// <summary>
  /// Decoupled interface to the device camera.
  /// </summary>
  public class WebCam : Service<WebCam> {
    [SerializeField] private bool useFrontFacing = false;
    [SerializeField] private bool isFullScreen   = true;

    /// <summary>
    /// Set in Unity inspector. Only applicable on devices with opposing cameras
    /// </summary>
    public bool UseFrontFacing { get { return useFrontFacing; } }

    /// <summary>
    /// Set in Unity inspector. If not true the application will need to set Width and Height
    /// </summary>
    public bool IsFullScreen { get { return isFullScreen; } }

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