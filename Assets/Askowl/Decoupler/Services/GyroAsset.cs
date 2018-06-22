using UnityEngine;

namespace CustomAsset.Mutable {
  /// <inheritdoc cref="Decoupled.GyroService" />
  [CreateAssetMenu(menuName = "Custom Assets/Device/Gyroscope"), ValueName("Device")]
  public class GyroAsset : OfType<Decoupled.GyroService> {
    /// <see cref="OfType{T}.Value"/>
    public Decoupled.GyroService Device { get { return Value; } set { Value = value; } }

    /// <inheritdoc />
    protected override void OnEnable() {
      base.OnEnable();
      Device = Decoupled.GyroService.Instance;
    }
  }
}