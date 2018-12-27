using System;
using Askowl;
using UnityEngine;

namespace Decoupled {
  /// <a href="">Interface to a device compass (magnetometer)</a> //#TBD#// <inheritdoc />
  [Serializable]
  // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
  public class CompassService : Service<CompassService> {
    [SerializeField, Tooltip("Not too small or there will be jitter")]
    private float minimumChange = 0.2f;

    private float  lastReading;
    private double lastTimestamp;

    /// <a href="">Call in implementation constructor</a> //#TBD#// <inheritdoc />
    protected override void Initialise() => Enabled = true;

    /// <a href="">Set if compass failed to initialise</a> //#TBD#//
    public virtual bool Offline => true;

    /// <a href="">Accuracy of compass at the moment (in degrees)</a> //#TBD#//
    public virtual float AccuracyDegrees => 0;

    /// <a href="">Which way (in degrees) the phone is pointing relative to magnetic north</a> //#TBD#//
    public virtual float MagneticHeading => 0;

    /// <a href="">Epoch time (seconds between 1/1/1970) since last reading</a> //#TBD#//
    public virtual double TimeStamp => 0;

    /// <a href="">Which way (in degrees) the phone is pointing relative to geographic north</a> //#TBD#//
    public virtual float TrueHeading => 0;

    /// <a href="">Sets or retrieves the enabled status of this device</a> //#TBD#//
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public virtual bool Enabled { get; set; }

    /// <a href="">Check if the magnetic heading has changed significantly</a> //#TBD#//
    public bool Changed() {
      if (Compare.AlmostEqual(lastTimestamp, TimeStamp) ||
          Compare.AlmostEqual(lastReading,   MagneticHeading, minimumChange)) { return false; }

      lastTimestamp = TimeStamp;
      lastReading   = MagneticHeading;
      return true;
    }
  }
}