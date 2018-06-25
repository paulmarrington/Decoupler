using Decoupled;
using UnityEngine;
using UnityEngine.UI;

namespace CustomAsset.Mutable {
  /// <inheritdoc />
  [CreateAssetMenu(menuName = "Custom Assets/Device/WebCam"), ValueName("Device")]
  public class WebCamAsset : OfType<WebCamService> {
    [SerializeField] private RawImage          background;
    [SerializeField] private AspectRatioFitter aspectRatioFitter;

    /// <see cref="OfType{T}.Value"/>
    public WebCamService Device { get { return Value; } private set { Value = value; } }

    public bool Ready { get { return Device.DidUpdateThisFrame; } }

    /// <inheritdoc />
    public override WebCamService Initialise() {
      Device             = WebCamService.Instance;
      background.texture = Device.Texture;
      return Device;
    }

    public void correctForDeviceRotation() {
      aspectRatioFitter.aspectRatio = Device.AspectRatio;

      int verticalMirror = Device.VerticalMirror ? -1 : 1;
      background.rectTransform.localScale = new Vector3(x: 1, y: verticalMirror, z: 1);

      int rotationAngle = Device.RotationAngle;
      background.rectTransform.localEulerAngles = new Vector3(x: 0, y: 0, z: -rotationAngle);
    }
  }
}