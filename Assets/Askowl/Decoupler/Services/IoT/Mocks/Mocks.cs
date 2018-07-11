using UnityEngine;

namespace Decoupled.Mock {
  public interface IMock{}

  public class Mocks : MonoBehaviour {
    [SerializeField] private IMock[] mocks;

    private void Awake() {
      foreach (IMock mock in mocks) {

      }
    }
  }
}