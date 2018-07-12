using UnityEngine;

namespace Decoupled.Mock {
  public class Mock<T> : MonoBehaviour where T : Service, new() {
    protected T MockService;

    protected virtual void Awake() {
      Service.RegisterAsMock(MockService = new T {Name = GetType().Name});
    }
  }
}