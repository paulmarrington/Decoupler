// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System.Collections;

namespace Decoupled {
  using JetBrains.Annotations;

  /// <inheritdoc />
  /// <summary>
  /// Interface for working with social networks like FaceBook, Google+, Twitter, Instagram and the like.
  /// </summary>
  /// <remarks><a href="http://decoupler.marrington.net#decoupledsocial">More...</a></remarks>
  public sealed class Social : Service<Social> {
    /// <summary>
    /// Return true if the interface is ready to use. There is often a delay between logging in and being able to use the interfaces
    /// </summary>
    /// <remarks><a href="http://decoupler.marrington.net#decoupledsocial">More...</a></remarks>
    [UsedImplicitly]
    public bool IsReady { get { return false; } }

    /// <summary>
    /// Returns true if player has signed in successfully to the network.
    /// </summary>
    /// <remarks><a href="http://decoupler.marrington.net#decoupledsocial">More...</a></remarks>
    [UsedImplicitly]
    public bool IsSignedIn { get { return false; } }

    /// <summary>
    /// Coroutine that returns once the interface is ready.
    /// </summary>
    /// <remarks><a href="http://decoupler.marrington.net#decoupledsocial">More...</a></remarks>
    /// <returns></returns>
    [UsedImplicitly]
    public IEnumerator Ready() {
      while (!IsReady) yield break;
    }

    /// <summary>
    /// Coroutine that returns once the player has been signed in successfully.
    /// </summary>
    /// <remarks><a href="http://decoupler.marrington.net#decoupledsocial">More...</a></remarks>
    /// <returns></returns>
    [UsedImplicitly]
    public IEnumerator SignedIn() {
      while (!IsSignedIn) yield break;
    }
  }
}