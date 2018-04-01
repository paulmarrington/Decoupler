using UnityEngine;

namespace Decoupled {
  using JetBrains.Annotations;

  public sealed class Analytics : Service<Analytics> {
    [UsedImplicitly]
    public void Error(string message, string action, [NotNull] params object[] more) {
      Event(name: "Error", action: action, result: message, more: more);
    }

    [UsedImplicitly]
    public void Event(string name, string action, string result, [NotNull] params object[] more) {
      Debug.Log("**** Event '" + name + "' -- action: " + action + ", result: " + result +
                ", more: "     + More(more));
    }

    [NotNull, UsedImplicitly]
    public string More([NotNull] params object[] list) {
      return string.Join(separator: ",",
                         value: System.Array.ConvertAll(array: list, converter: x => x.ToString()));
    }
  }
}