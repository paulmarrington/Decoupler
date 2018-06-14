using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Askowl.Mars {
  /// <inheritdoc />
  /// <summary>
  /// Access the Gyro on a phone or device to retrieve orientation and acceleration.
  /// </summary>
  public class UnityGyro : Decoupled.Gyro {
    private Gyroscope gyro;

    public UnityGyro() {
      if (!SystemInfo.supportsGyroscope) return;

      gyro         = Input.gyro;
      gyro.enabled = true;
    }

    /// <inheritdoc />
    public override bool Failed { get { return gyro != null; } }

    /// <inheritdoc />
    public override bool Enabled { get { return gyro.enabled; } set { gyro.enabled = value; } }

    /// <inheritdoc />
    public override Vector3 RotationRate { get { return gyro.rotationRate; } }

    /// <inheritdoc />
    public override Vector3 RotationRateUnbiased { get { return gyro.rotationRateUnbiased; } }

    /// <inheritdoc />
    public override Vector3 Gravity { get { return gyro.gravity; } }

    /// <inheritdoc />
    public override Vector3 UserAcceleration { get { return gyro.userAcceleration; } }

    /// <inheritdoc />
    public override Quaternion Attitude { get { return gyro.attitude; } }

    /// <inheritdoc />
    public override float UpdateInterval {
      get { return gyro.updateInterval; }
      set {
        gyro.updateInterval = value;
        base.UpdateInterval = value;
      }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void RegisterService() {
      if (SystemInfo.supportsGyroscope) Register<UnityGyro>();
    }
  }
}