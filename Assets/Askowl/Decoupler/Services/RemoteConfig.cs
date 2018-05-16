namespace Decoupled {
  using JetBrains.Annotations;

  /// <inheritdoc />
  /// <summary>
  /// Interface for an app to retrieve configuration from a remote source.
  /// </summary>
  /// <remarks><a href="http://decoupler.marrington.net#decoupledremoteconfig">More...</a></remarks>
  [UsedImplicitly]
  public class RemoteConfig : Service<RemoteConfig> { }
}