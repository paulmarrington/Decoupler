using System;
using Askowl;
using Decoupled;
using UnityEngine;

namespace CustomAsset.Mutable {
  /// <inheritdoc cref="Decoupled.GyroService" />
  [CreateAssetMenu(menuName = "Custom Assets/Device/Gyroscope"), ValueName("Device")]
  public class GyroAsset : OfType<GyroService> {
    public static GyroAsset Instance { get { return Instance<GyroAsset>(); } }

    /// <see cref="OfType{T}.Value"/>
    public GyroService Device { get { return Value; } set { Value = value; } }

    private float settleTime;
    private bool  settled;

    public bool Ready {
      get {
        if (settled) return true;

        if (Device.Attitude == Quaternion.identity) return false;

        settleTime = Time.realtimeSinceStartup - settleTime;
        return (settled = true);
      }
    }

    public float SecondsSettlingTime { get { return settleTime; } }

    /// Gyro is right-handed while Unity is left-handed.
    public Quaternion Attitude { get { return RightToLeftHanded(Input.gyro.attitude); } }

    private Quaternion RightToLeftHanded(Quaternion gyroAttitude) {
      gyroAttitude.z = -gyroAttitude.z;
      gyroAttitude.w = -gyroAttitude.w;
      return gyroAttitude;
    }

    /// <inheritdoc />
    public override GyroService Initialise() {
      Device     = GyroService.Instance;
      settleTime = Time.realtimeSinceStartup;
      return Device;
    }
  }
}