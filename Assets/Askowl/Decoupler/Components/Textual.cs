// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Decoupled {
  /// <inheritdoc />
  /// <summary>
  /// Interface to generic UI text functions. Defaults to <see cref="UnityEngine.UI.Text"/>
  /// </summary>
  /// <remarks><a href="http://decoupler.marrington.net#decoupledtext">More...</a></remarks>
  public partial class Textual : ComponentDecoupler<Textual> {
    public interface Interface {
      string text { get; set; }
    }

    private static Interface backer {get{return (Interface) interfaceData; }}

    protected override Type defaultComponent { get { return typeof(Text); } }

    public string text { get { return backer.text; } set { backer.text = value; } }
  }

  public partial class Textual {
    [RuntimeInitializeOnLoadMethod]
    private static void UnityTextInitialise() {
      // ReSharper disable once SuspiciousTypeConversion.Global
      initialisers += (my) => interfaceData = interfaceData ?? (Interface) my.GetComponent<Text>();
    }
  }
}