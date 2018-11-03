using System;
using UnityEngine;

namespace Decoupled {
  /// <a href=""></a> //#TBD#// <inheritdoc />
  [Serializable] public class GyroService : Service<GyroService> {
    /// <a href=""></a> //#TBD#// <inheritdoc />
    protected override void Initialise() {
      Enabled = true; // Tell the gyroscope to start spinning :)
    }

    /// <a href="">Set if Gyro failed to initialise</a> //#TBD#//
    public virtual bool Offline => true;

    /// <a href="">Returns rotation rate as measured by the device's gyroscope.</a> //#TBD#//
    public virtual Vector3 RotationRate => Vector3.zero;

    /// <a href="">Returns unbiased rotation rate as measured by the device's gyroscope.</a> //#TBD#//
    public virtual Vector3 RotationRateUnbiased => Vector3.zero;

    /// <a href="">Returns the gravity acceleration vector expressed in the device's reference frame.</a> //#TBD#//
    public virtual Vector3 Gravity => Vector3.zero;

    /// <a href="">Returns the acceleration that the user is giving to the device.</a> //#TBD#//
    public virtual Vector3 UserAcceleration => Vector3.zero;

    /// <a href="">Returns the attitude (ie, orientation in space) of the device.</a> //#TBD#//
    public virtual Quaternion Attitude => Quaternion.identity;

    // ReSharper disable once UnusedMemberInSuper.Global
    /// <a href="">Some gyroscope services allow an update interval to be set to save polling</a> //#TBD#//
    public virtual float UpdateIntervalInSeconds { get; set; }

    /// <a href="">Sets or retrieves the enabled status of this gyroscope.</a> //#TBD#//
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