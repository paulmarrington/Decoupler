using Decoupled;
using UnityEngine;

namespace CustomAsset.Mutable {
  /// <inheritdoc />
  [CreateAssetMenu(menuName = "Custom Assets/Device/WebCam"), ValueName("Device")]
  public class WebCamAsset : OfType<WebCamService> {
    /// <see cref="OfType{T}.Value"/>
    public WebCamService Device { get { return Value; } private set { Value = value; } }

    /// <inheritdoc />
    public override WebCamService Initialise() { return Device = WebCamService.Instance; }
  }
}