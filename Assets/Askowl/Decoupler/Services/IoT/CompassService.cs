using System;
using Askowl;
using UnityEngine;

namespace Decoupled {
  /// <inheritdoc />
  /// <summary>
  /// Interface to a device compass (magnetometer).
  /// </summary>
  /// <remarks><a href="http://unitydoc.marrington.net/Mars#service">More...</a></remarks>
  [Serializable]
  public class CompassService : Service<CompassService> {
    [SerializeField, Tooltip("Not too small or there will be jitter")]
    private float minimumChange = 0.2f;

    private float  lastReading;
    private double lastTimestamp;

    /// <inheritdoc />
    /// Call in implementation constructor
    protected override void Initialise() { Enabled = true; }

    /// Set if compass failed to initialise
    public virtual bool Offline => true;

    ///  Accuracy of compass at the moment (in degrees)
    public virtual float AccuracyDegrees => 0;

    /// Which way (in degrees) the phone is pointing relative to magnetic north
    public virtual float MagneticHeading => 0;

    /// Epoch time (seconds between 1/1/1970) since last reading
    public virtual double TimeStamp => 0;

    /// Which way (in degrees) the phone is pointing relative to geographic north
    public virtual float TrueHeading => 0;

    /// Sets or retrieves the enabled status of this device.
    public virtual bool Enabled { get; set; }

    /// Check if the magnetic heading has changed significantly.
    public bool Changed() {
      if (Compare.AlmostEqual(lastTimestamp, TimeStamp) ||
          Compare.AlmostEqual(lastReading,   MagneticHeading, minimumChange)) {
        return false;
      }

      lastTimestamp = TimeStamp;
      lastReading   = MagneticHeading;
      return true;
    }
  }
}