using Decoupled;
using UnityEngine;

namespace CustomAsset.Mutable {
  /// <inheritdoc cref="Decoupled.GyroService" />
  [CreateAssetMenu(menuName = "Custom Assets/Device/Gyroscope"), ValueName("Device")]
  public class GyroAsset : OfType<GyroService> {
    /// <see cref="OfType{T}.Value"/>
    public GyroService Device { get { return Value; } set { Value = value; } }

    private float settleTime;
    private bool  settled;
    private float calibrationAngleY;
    private Quaternion rotation;

    public bool Ready {
      get {
        if (settled) return true;
        if (Time.realtimeSinceStartup < settleTime) return false;

        rotation = Quaternion.Euler(x: 90, y: 180, z: 180);
        float webcamAngleY = (Device.Attitude * rotation).eulerAngles.y;
        calibrationAngleY -= webcamAngleY;
        return (settled = true);
      }
    }

    public Quaternion Attitude {
      get {
        return Device.Attitude * rotation;
      }
    }

    /// <inheritdoc />
    public override GyroService Initialise() {
      settleTime = Time.realtimeSinceStartup + 1;
      return Device = GyroService.Instance;
    }

    public void Calibrate(float unityCameraAngleY) { calibrationAngleY = unityCameraAngleY; }
  }
}