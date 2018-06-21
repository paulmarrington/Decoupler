using UnityEngine;
using UnityEngine.Serialization;

namespace Askowl {
  /// <inheritdoc />
  /// <summary>
  /// WebCam custom asset
  /// </summary>
  [CreateAssetMenu(menuName = "Custom Assets/Device/WebCam")]
  public class WebCam : CustomAsset.Mutable.OfType<Decoupled.WebCamService> {
    [HideInInspector] private Decoupled.WebCamService value;

    [SerializeField] private Decoupled.WebCamService device;

    /// <summary>
    /// Different name for Value
    /// </summary>
    public Decoupled.WebCamService Device { get; private set; }

    /// <inheritdoc />
    protected override void OnEnable() {
      base.OnEnable();
      Device = Value = Decoupled.WebCamService.Instance;
    }
  }
}