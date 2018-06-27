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

    private bool settled;
//    private Quaternion calibration;

    public bool Ready {
      get {
        if (settled) return true;

        return (settled = (Time.realtimeSinceStartup > settleTime));
      }
    }

    public Quaternion Attitude {
//      get { return RightToLeftHanded(calibration * Input.gyro.attitude); }
      get { return RightToLeftHanded(Input.gyro.attitude); }
    }

    private Quaternion RightToLeftHanded(Quaternion q) {
      return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    /// <inheritdoc />
    public override GyroService Initialise() {
      settleTime = Time.realtimeSinceStartup + 1;
      return Device = GyroService.Instance;
    }

    public Quaternion Calibrate() {
      var fw = (Input.gyro.attitude) * (-Vector3.forward);
      fw.z = 0;

      Quaternion calibration = (fw == Vector3.zero)
                                 ? Quaternion.identity
                                 : Quaternion.FromToRotation(Vector3.up, fw);

      Quaternion baseOrientation = Quaternion.Euler(x: 90, y: 0, z: 0);
      calibration = Quaternion.Inverse(baseOrientation) * Quaternion.Inverse(calibration);
      return RightToLeftHanded(calibration);
    }
  }
}