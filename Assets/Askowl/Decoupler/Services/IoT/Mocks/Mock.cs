using UnityEngine;

namespace Decoupled.Mock {
  /// <inheritdoc />
  /// <summary>
  /// Monobehaviour that when active in a scene will replace services with a mock
  /// that will run without a real-world device.
  /// </summary>
  /// <typeparam name="T">Class of Mock Service (Child of Mock)</typeparam>
  /// <remarks><a href="http://unitydoc.marrington.net/Mars#mocking">More...</a></remarks>
  public class Mock<T> : MonoBehaviour where T : Service, new() {
    /// <summary>
    /// An instance of the Mock service created here and used to replace any
    /// default service.
    /// </summary>
    protected T MockService;

    /// <summary>
    /// Register the mock as the service to use for this hardware
    /// </summary>
    protected virtual void Awake() => Service.RegisterAsMock(MockService = new T {Name = GetType().Name});
  }
}