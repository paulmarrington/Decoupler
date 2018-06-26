using Decoupled;
using UnityEngine;

namespace CustomAsset.Mutable {
  /// <inheritdoc cref="Decoupled.GPSService" />
  [CreateAssetMenu(menuName = "Custom Assets/Device/GPS"), ValueName("Device")]
// ReSharper disable once InconsistentNaming
  public class GPSAsset : OfType<GPSService> {
    /// <see cref="OfType{T}.Value"/>
    public GPSService Device { get { return Value; } set { Value = value; } }

    /// <inheritdoc />
    public override GPSService Initialise() { return Device = GPSService.Instance; }
  }
}