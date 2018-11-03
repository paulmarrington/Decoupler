// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

namespace Decoupled {
  using System.Collections;

  /// <a href=""></a> //#TBD#// <inheritdoc />
  public sealed class Social : Service<Social> {
    /// <a href=""></a> //#TBD#//
    public bool IsReady => false;

    /// <a href=""></a> //#TBD#//
    public bool IsSignedIn => false;

    /// <a href=""></a> //#TBD#//
    public IEnumerator Ready() {
      while (!IsReady) yield break;
    }

    /// <a href=""></a> //#TBD#//
    public IEnumerator SignedIn() {
      while (!IsSignedIn) yield break;
    }
  }
}