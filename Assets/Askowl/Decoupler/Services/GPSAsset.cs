using Askowl;
using UnityEngine;

namespace CustomAsset.Mutable {
  /// <inheritdoc cref="Decoupled.GPSService" />
  [CreateAssetMenu(menuName = "Custom Assets/Device/GPS"), ValueName("Device")]
// ReSharper disable once InconsistentNaming
  public class GPSAsset : OfType<Decoupled.GPSService>, IPolling {
    /// <see cref="OfType{T}.Value"/>
    public Decoupled.GPSService Device { get { return Value; } set { Value = value; } }

    /// <inheritdoc />
    protected override void OnEnable() {
      base.OnEnable();
      Device = Decoupled.GPSService.Instance;
    }

    /// <inheritdoc />
    public void Poll() { Emitter.Fire(); }
  }
}