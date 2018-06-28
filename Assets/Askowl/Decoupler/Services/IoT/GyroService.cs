using System;
using UnityEngine;

namespace Decoupled {
  /// <inheritdoc />
  /// <summary>
  /// Interface to a device gyroscope.
  /// </summary>
  [Serializable]
  public class GyroService : Service<GyroService> {
    /// <inheritdoc />
    /// Call in implementation constructor
    protected override void Initialise() {
      Enabled = true; // Tell the gyroscope to start spinning :)
    }

    /// Set if Gyro failed to initialise
    public virtual bool Offline { get { return true; } }

    /// <summary>
    ///   <para>Returns rotation rate as measured by the device's gyroscope.</para>
    /// </summary>
    public virtual Vector3 RotationRate { get { return Vector3.zero; } }

    /// <summary>
    ///   <para>Returns unbiased rotation rate as measured by the device's gyroscope.</para>
    /// </summary>
    public virtual Vector3 RotationRateUnbiased { get { return Vector3.zero; } }

    /// <summary>
    ///   <para>Returns the gravity acceleration vector expressed in the device's reference frame.</para>
    /// </summary>
    public virtual Vector3 Gravity { get { return Vector3.zero; } }

    /// <summary>
    ///   <para>Returns the acceleration that the user is giving to the device.</para>
    /// </summary>
    public virtual Vector3 UserAcceleration { get { return Vector3.zero; } }

    /// <summary>
    ///   <para>Returns the attitude (ie, orientation in space) of the device.</para>
    /// </summary>
    public virtual Quaternion Attitude { get { return Quaternion.identity; } }

    public virtual float UpdateIntervalInSeconds { get; set; }

    /// <summary>
    ///   <para>Sets or retrieves the enabled status of this gyroscope.</para>
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
    public override int GetHashCode() { return Attitude.GetHashCode(); }

    public override string ToString() { return Attitude.eulerAngles.ToString(); }
  }
}