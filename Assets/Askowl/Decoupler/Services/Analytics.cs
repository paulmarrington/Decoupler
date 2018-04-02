namespace Decoupled {
  using System;
  using JetBrains.Annotations;
  using UnityEngine;

  public class Analytics : Service<Analytics> {
    [UsedImplicitly]
    public virtual void Error(string name, string message, [NotNull] params object[] more) {
      Event(name: name, action: "Error", result: message, more: more);
    }

    [UsedImplicitly]
    public virtual void Event(string                    name, string action, string result,
                              [NotNull] params object[] more) {
      Debug.Log("**** Event '" + name + "' -- action: " + action + ", result: " + result +
                ", more: "     + More(more));
    }

    [NotNull, UsedImplicitly]
    public string More([NotNull] params object[] list) {
      return string.Join(separator: ",",
                         value: Array.ConvertAll(array: list, converter: x => x.ToString()));
    }

    public enum Genders {
      Male,
      Female,
      Other,
      Unknown
    }

    public virtual Genders Gender { get { return gender; } set { gender = value; } }
    protected      Genders gender = Genders.Unknown;

    public virtual int BirthYear { get { return birthYear; } set { birthYear = value; } }
    protected      int birthYear;
  }
}