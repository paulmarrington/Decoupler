using Askowl;
using UnityEngine;

namespace CustomAsset.Mutable {
  /// <inheritdoc cref="Gyro" />
  [CreateAssetMenu(menuName = "Custom Assets/Device/Gyroscope"), ValueName("Device")]
  public class GyroAsset : OfType<Decoupled.GyroService>, IPolling {
    /// <see cref="OfType{T}.Value"/>
    public Decoupled.GyroService Device { get { return Value; } set { Value = value; } }

    /// <inheritdoc />
    protected override void OnEnable() {
      base.OnEnable();
      Device = Decoupled.GyroService.Instance;
    }

    /// <inheritdoc />
    public void Poll() { Emitter.Fire(); }
  }
}