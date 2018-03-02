using System.Collections;

namespace Decoupled {
  using JetBrains.Annotations;

  public sealed class Social : Service<Social> {
    [UsedImplicitly]
    public bool IsReady { get { return false; } }

    [UsedImplicitly]
    public bool IsSignedIn { get { return false; } }

    [UsedImplicitly]
    public IEnumerator Ready() { yield break; }

    [UsedImplicitly]
    public IEnumerator SignedIn() { yield break; }
  }
}