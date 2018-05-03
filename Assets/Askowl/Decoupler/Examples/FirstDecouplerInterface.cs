#if UNITY_EDITOR && AskowlDecoupler
namespace Decoupled {
  /// <inheritdoc />
  /// <summary>
  /// Default interface for the unit test
  /// </summary>
  public class FirstDecouplerInterface : Service<FirstDecouplerInterface> {
    /// <summary>
    /// something to check which instance is used.
    /// </summary>
    protected int Number;

    internal virtual void Entry1(int number) { Number = number; }

    internal int Entry2() { return Number; }
  }
}
#endif