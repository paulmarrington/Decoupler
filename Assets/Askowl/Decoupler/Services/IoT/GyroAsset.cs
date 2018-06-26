using Decoupled;
using UnityEngine;

namespace CustomAsset.Mutable {
  /// <inheritdoc cref="Decoupled.GyroService" />
  [CreateAssetMenu(menuName = "Custom Assets/Device/Gyroscope"), ValueName("Device")]
  public class GyroAsset : OfType<GyroService> {
    /// <see cref="OfType{T}.Value"/>
    public GyroService Device { get { return Value; } set { Value = value; } }

    private float      settleTime;
    private bool       settled;
    private Vector3    unityCameraForward;
    private Quaternion rotation, cameraBase;

    public bool Ready {
      get {
        if (settled) return true;
        if (Time.realtimeSinceStartup < settleTime) return false;

        var fw = (Input.gyro.attitude) * (-Vector3.forward);
        fw.z = 0;

        Quaternion calibration = (fw == Vector3.zero)
                                   ? Quaternion.identity
                                   : Quaternion.FromToRotation(Vector3.up, fw);

        fw   = unityCameraForward;
        fw.y = 0;

        cameraBase = (fw == Vector3.zero)
                       ? Quaternion.identity
                       : Quaternion.FromToRotation(Vector3.forward, fw);

        Quaternion baseOrientation = Quaternion.Euler(90, 0, 0);
        rotation = Quaternion.Inverse(baseOrientation) * Quaternion.Inverse(calibration);

        return (settled = true);
      }
    }

    public Quaternion Attitude {
      get { return cameraBase * (RightToLeftHanded(rotation * Input.gyro.attitude)); }
    }

    private Quaternion RightToLeftHanded(Quaternion q) {
      return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    /// <inheritdoc />
    public override GyroService Initialise() {
      settleTime = Time.realtimeSinceStartup + 1;
      return Device = GyroService.Instance;
    }

    public void Calibrate(Vector3 cameraForward) { unityCameraForward = cameraForward; }
  }
}