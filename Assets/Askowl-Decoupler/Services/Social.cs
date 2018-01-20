using System.Collections;
using System.Collections.Generic;

namespace Decoupled {
  public class Social : Decoupled.Service<Social> {

    public virtual bool IsReady { get { return false; } }

    public virtual bool IsSignedIn { get { return false; } }

    public virtual IEnumerator Ready() {
      yield break;
    }

    public virtual IEnumerator SignedIn() {
      yield break;
    }
  }
}