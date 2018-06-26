using System;
using Askowl;
using UnityEngine;

namespace Decoupled {
  /// <inheritdoc />
  /// <summary>
  /// Interface to a device compass (magnetometer).
  /// </summary>
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
    public virtual bool Offline { get { return true; } }

    ///  Accuracy of compass at the moment (in degrees)
    public virtual float AccuracyDegrees { get { return 0; } }

    /// Which way (in degrees) the phone is pointing relative to magnetic north
    public virtual float MagneticHeading { get { return 0; } }

    /// Epoch time (seconds between 1/1/1970) since last reading
    public virtual double TimeStamp { get { return 0; } }

    /// Which way (in degrees) the phone is pointing relative to geographic north
    public virtual float TrueHeading { get { return 0; } }

    /// <summary>
    ///   <para>Sets or retrieves the enabled status of this gyroscope.</para>
    /// </summary>
    public virtual bool Enabled { get; set; }

    /// <inheritdoc />
    public override bool Equals(object other) {
      if (Compare.AlmostEqual(lastTimestamp, TimeStamp) ||
          Compare.AlmostEqual(lastReading,   MagneticHeading, minimumChange)) return true;

      lastTimestamp = TimeStamp;
      lastReading   = MagneticHeading;
      return false;
    }

    /// <inheritdoc />
    public override int GetHashCode() { return MagneticHeading.GetHashCode(); }
  }
}