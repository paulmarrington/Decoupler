#if UNITY_EDITOR && AskowlDecoupler
namespace Decoupled {
  /// <a href="">Default interface for the unit test</a> //#TBD#// <inheritdoc />
  public class FirstDecouplerInterface : Service<FirstDecouplerInterface> {
    /// <a href="">something to check which instance is used</a> //#TBD#//
    protected int Number;

    internal virtual void Entry1(int number) { Number = number; }

    internal int Entry2() => Number;
  }
}
#endif