using System;
using UnityEngine;

namespace Decoupled {
  /// <inheritdoc />
  /// <summary>
  /// Interface to a device gyroscope.
  /// </summary>
  /// <remarks><a href="http://unitydoc.marrington.net/Mars#service-2">More...</a></remarks>
  [Serializable]
  public class GyroService : Service<GyroService> {
    /// <inheritdoc />
    /// Call in implementation constructor
    protected override void Initialise() {
      Enabled = true; // Tell the gyroscope to start spinning :)
    }

    /// Set if Gyro failed to initialise
    public virtual bool Offline => true;

    /// <summary>
    ///   Returns rotation rate as measured by the device's gyroscope.
    /// </summary>
    public virtual Vector3 RotationRate => Vector3.zero;

    /// <summary>
    ///   Returns unbiased rotation rate as measured by the device's gyroscope.
    /// </summary>
    public virtual Vector3 RotationRateUnbiased => Vector3.zero;

    /// <summary>
    ///   Returns the gravity acceleration vector expressed in the device's reference frame.
    /// </summary>
    public virtual Vector3 Gravity => Vector3.zero;

    /// <summary>
    ///   Returns the acceleration that the user is giving to the device.
    /// </summary>
    public virtual Vector3 UserAcceleration => Vector3.zero;

    /// <summary>
    ///   Returns the attitude (ie, orientation in space) of the device.
    /// </summary>
    public virtual Quaternion Attitude => Quaternion.identity;

    // ReSharper disable once UnusedMemberInSuper.Global
    /// <summary>
    /// Some gyroscope services allow an update interval to be set to save polling
    /// </summary>
    public virtual float UpdateIntervalInSeconds { get; set; }

    /// <summary>
    ///   Sets or retrieves the enabled status of this gyroscope.
    /// </summary>
    public virtual bool Enabled { get; set; }

    private Quaternion lastReading;

    /// <inheritdoc />
    public override bool Equals(object other) {
      if (Attitude == lastReading) return true;

      lastReading = Attitude;
      return false;
    }

    /// <inheritdoc />
    public override int GetHashCode() => Attitude.GetHashCode();

    /// <inheritdoc />
    public override string ToString() => Attitude.eulerAngles.ToString();
  }
}