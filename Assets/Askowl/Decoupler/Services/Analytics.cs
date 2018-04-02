﻿namespace Decoupled {
  using System;
  using System.Collections.Generic;
  using JetBrains.Annotations;
  using UnityEngine;

  public class Analytics : Service<Analytics> {
    [UsedImplicitly]
    public virtual void Error(string                    name,
                              string                    message,
                              [NotNull] params object[] more) {
      Event(name: name, action: "Error", result: message, more: more);
    }

    [UsedImplicitly]
    public virtual void Event(string                    name,
                              string                    action,
                              string                    result,
                              [NotNull] params object[] more) {
      Debug.Log("**** Event '" + name + "' -- action: " + action + ", result: " + result +
                ", more: "     + More(more));
    }

    [NotNull, UsedImplicitly]
    public string More([NotNull] params object[] list) {
      return string.Join(separator: ",",
                         value: Array.ConvertAll(array: list, converter: x => x.ToString()));
    }

    public Dictionary<string, object> ToDictionary(string                    action,
                                                   string                    result,
                                                   [NotNull] params object[] more) {
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

    public virtual string Gender { private get; set; }

    public virtual int BirthYear { private get; set; }
  }
}