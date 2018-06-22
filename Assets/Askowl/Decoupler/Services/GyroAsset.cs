using Decoupled;
using UnityEngine;

namespace CustomAsset.Mutable {
  /// <inheritdoc cref="Decoupled.GyroService" />
  [CreateAssetMenu(menuName = "Custom Assets/Device/Gyroscope"), ValueName("Device")]
  public class GyroAsset : OfType<GyroService> {
    /// <see cref="OfType{T}.Value"/>
    public GyroService Device { get { return Value; } set { UnconditionalSet(value); } }

    /// <inheritdoc />
    public override GyroService Initialise() { return Device = GyroService.Instance; }
  }
}