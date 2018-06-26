using Decoupled;
using UnityEngine;

namespace CustomAsset.Mutable {
  /// <inheritdoc cref="Decoupled.GyroService" />
  [CreateAssetMenu(menuName = "Custom Assets/Device/Compass"), ValueName("Device")]
  public class CompassAsset : OfType<CompassService> {
    /// <see cref="OfType{T}.Value"/>
    public CompassService Device { get { return Value; } set { Value = value; } }

    private float      settleTime;
    private bool       settled;
    private Quaternion magneticHeading;

    public bool Ready {
      get {
        if (settled) return true;
        if (Time.realtimeSinceStartup < settleTime) return false;

        return (settled = true);
      }
    }
//
//    public Quaternion MagneticHeading {
//      get {
//        if (!Device.Equals(null)) {
//          var newHeading = Quaternion.Euler(0, -Device.MagneticHeading, 0);
//          magneticHeading = newHeading;
//        }
//        return magneticHeading;
//      }
//    }

    /// <inheritdoc />
    public override CompassService Initialise() {
      settleTime = Time.realtimeSinceStartup + 1;
      return Device = CompassService.Instance;
    }
  }
}