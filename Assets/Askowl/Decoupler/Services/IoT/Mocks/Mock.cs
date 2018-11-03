namespace Decoupled.Mock {
  using UnityEngine;

  /// <a href="">MonoBehaviour that when active in a scene will replace services with a mock that will run without a real-world device</a> //#TBD#// <inheritdoc />
  public class Mock<T> : MonoBehaviour where T : Service, new() {
    /// <a href="">An instance of the Mock service created here and used to replace any default service</a> //#TBD#//
    protected T MockService;

    /// <a href="">Register the mock as the service to use for this hardware</a> //#TBD#//
    protected virtual void Awake() => Service.RegisterAsMock(MockService = new T { Name = GetType().Name });
  }
}