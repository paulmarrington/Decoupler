using UnityEngine;

namespace CustomAsset.Mutable {
  /// <inheritdoc />
  [CreateAssetMenu(menuName = "Custom Assets/Device/WebCam"), ValueName("Device")]
  public class WebCamAsset : OfType<Decoupled.WebCamService> {
    /// <see cref="OfType{T}.Value"/>
    public Decoupled.WebCamService Device { get { return Value; } private set { Value = value; } }

    /// <inheritdoc />
    protected override void OnEnable() {
      base.OnEnable();
      Device = Decoupled.WebCamService.Instance;
    }
  }
}