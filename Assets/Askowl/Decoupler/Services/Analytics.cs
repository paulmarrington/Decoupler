namespace Decoupled {
  using System;
  using System.Collections.Generic;
  using JetBrains.Annotations;
  using UnityEngine;

  // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
  /// <inheritdoc />
  /// <summary>
  /// Analytics decoupled interface. Concrete implementations may use Unity, Firebase or a host of others.
  /// </summary>
  public class Analytics : Service<Analytics> {
    /// <summary>
    /// Default constructor registers interest in Authentication events.
    /// </summary>
    public Analytics() {
      Authentication.OnBirthYearChange += OnBirthYearChange;
      Authentication.OnGenderChange    += OnGenderChange;
    }

    ~Analytics() {
      Authentication.OnBirthYearChange -= OnBirthYearChange;
      Authentication.OnGenderChange    -= OnGenderChange;
    }

    /// <summary>
    /// Errors are specific events that most services deal with in a way that gives them more attention.
    /// </summary>
    /// <param name="name">Name for component in which the error occurred</param>
    /// <param name="message">Detailed error message</param>
    /// <param name="more">Additional information that make be useful</param>
    [UsedImplicitly]
    public virtual void Error(string          name,
                              string          message,
                              params object[] more) {
      Event(name: name, action: "Error", result: message, more: more);
    }

    /// <summary>
    /// Record events of interest.
    /// </summary>
    /// <param name="name">Name for component or area to which the event refers to</param>
    /// <param name="action">What is going to happen next</param>
    /// <param name="result">The result of the action on the game</param>
    /// <param name="more">Additional information that make be useful</param>
    [UsedImplicitly]
    public virtual void Event(string          name,
                              string          action,
                              string          result,
                              params object[] more) {
      Debug.Log("**** Event '" + name + "' -- action: " + action + ", result: " + result +
                ", more: "     + More(more));
    }

    /// <summary>
    /// Static helper function that will take a list of objects and turns then into a comma separated string
    /// </summary>
    /// <param name="list">The list to convert</param>
    /// <returns>A string containing the csv</returns>
    [NotNull, UsedImplicitly]
    public static string More(params object[] list) {
      string[] items = Array.ConvertAll(array: list, converter: x => x.ToString());
      return string.Join(separator: ",", value: items);
    }

    /// <summary>
    /// static helper function that converts an array of objects into key value pairs. The value is only taken from the list if the key ends in equals (=). It is used by services that expect data in dictionary form.
    /// </summary>
    /// <param name="action">Action parameter as provided by <see cref="Event"/></param>
    /// <param name="result">Results parameter as provided by <see cref="Event"/></param>
    /// <param name="more">More parameter list as provided by <see cref="Event"/></param>
    /// <returns>A dictionary with string keys</returns>
    [UsedImplicitly]
    public static Dictionary<string, object> ToDictionary(string          action,
                                                          string          result,
                                                          params object[] more) {
      Dictionary<string, object> dictionary = new Dictionary<string, object> {
        {"action", action}, {"result", result}
      };

      for (int i = 0; i < more.Length; i++) {
        string key = more[i].ToString();

        object value = null;

        if (key.EndsWith("=")) {
          key   = key.Substring(0, key.Length - 1);
          value = more[++i];
        }

        dictionary[key] = value;
      }

      return dictionary;
    }

    /// <summary>
    /// Gender of the player - as provided by the Authentication service.
    /// </summary>
    protected virtual string Gender { [UsedImplicitly] get; private set; }

    /// <summary>
    /// Year of birth of the player - as provided by the Authentication service.
    /// </summary>
    protected virtual int BirthYear { [UsedImplicitly] get; private set; }

    private void OnBirthYearChange(int birthYear) { BirthYear = birthYear; }

    private void OnGenderChange(string gender) { Gender = gender; }
  }
}