﻿using Decoupled;

internal sealed class FirstDecouplerService : FirstDecouplerInterface {
  internal override void Entry1(int number) { Number = number * 2; }
}