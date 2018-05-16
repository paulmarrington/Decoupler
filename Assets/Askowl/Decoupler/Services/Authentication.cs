// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System.Collections;
using System;

namespace Decoupled {
  using JetBrains.Annotations;

  /// <inheritdoc />
  /// <summary>
  /// Decoupled interface for authentication services. This is once case here a game may have more than one service active.
  /// </summary>
  /// <remarks><a href="http://decoupler.marrington.net#decoupledauthentication">More...</a></remarks>
  public sealed class Authentication : Service<Authentication> {
    private sealed class User {
      // ReSharper disable NotAccessedField.Global
      // ReSharper disable NotAccessedField.Local
      // ReSharper disable UnusedMember.Local

      internal string Name        = "guest";
      internal string Email       = "";
      internal string PhotoUrl    = "";
      internal string PhoneNumber = "";
      internal string ProviderId  = "";
      internal string UserId      = "";
      internal string Gender      = "Unknown";
      internal bool   IsVerified  = false;
      internal bool   IsLoggedIn  = false;
      internal int    BirthYear   = 0;
      internal object MetaData    = null;

      // ReSharper restore UnusedMember.Local
      // ReSharper restore NotAccessedField.Local
      // ReSharper restore NotAccessedField.Global
    }

    private User user = new User();

    [UsedImplicitly]
    public IEnumerator CreateUser(string         email, string password,
                                  Action<string> error = null) {
      user.Name = user.Email = email;
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator UpdateProfile(string         displayName,
                                     string         photoUrl = null,
                                     Action<string> error    = null) {
      user.Name     = displayName;
      user.PhotoUrl = photoUrl;
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator SignIn(string         email, string password,
                              Action<string> error = null) {
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator SignIn(object credential, Action<string> error = null) { yield return null; }

    [UsedImplicitly]
    public void SignOut() { user = new User(); }

    [UsedImplicitly]
    public IEnumerator Anonymous(Action<string> error = null) { yield return null; }

    [UsedImplicitly]
    public IEnumerator LinkWith(object credential, Action<string> error = null) {
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator Reload(Action<string> error = null) { yield return null; }

    [UsedImplicitly]
    public IEnumerator GetToken(Action<string> setToken, Action<string> error = null) {
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator DeleteUser(Action<string> error = null) { yield return null; }

    [UsedImplicitly]
    public IEnumerator ProvidersFor(string         email, Action<string[]> providers,
                                    Action<string> error = null) {
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator PasswordReset(string email, Action<string> error = null) {
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator VerifyPhoneNumber(string         number, Action<string> setId,
                                         Action<string> error = null) {
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator PhoneSignIn(string         phoneAuthVerificationId,
                                   string         receivedCode,
                                   Action<string> error = null) {
      yield return null;
    }
  }
}