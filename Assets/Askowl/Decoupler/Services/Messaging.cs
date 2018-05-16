﻿namespace Decoupled {
  using JetBrains.Annotations;

  /// <summary>
  /// Remote messaging interface - player to player communications
  /// </summary>
  /// <remarks><a href="http://decoupler.marrington.net#decoupledmessaging">More...</a></remarks>
  [UsedImplicitly]
  public class Messaging : Service<Messaging> { }
}