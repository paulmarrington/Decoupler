// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

using System.Collections;
using System;
using Object = UnityEngine.Object;

namespace Decoupled {
  using JetBrains.Annotations;

  /// <inheritdoc />
  /// <summary>
  /// Decoupled interface for authentication services. This is once case here a game may have more than one service active.
  /// </summary>
  /// <remarks><a href="http://decoupler.marrington.net#decoupledauthentication">More...</a></remarks>
  public sealed class Authentication : Service<Authentication> {
    public class User {
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
    }

    private AuthenticationAsset user = CustomAsset.Base.Instance<AuthenticationAsset>();

    [UsedImplicitly]
    public IEnumerator CreateUser(string         email, string password,
                                  Action<string> error = null) {
      user.Name = user.Email = email;
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator UpdateProfile(string         displayName,
                                     Action<string> error = null) {
      user.Name = displayName;
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
    public void SignOut() {
      Object.Destroy(user);
      user = CustomAsset.Base.Instance<AuthenticationAsset>();
    }

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

  /// <inheritdoc />
  /// <summary>
  /// Contains valuable information recorded when a player logs in.
  /// </summary>
  public class AuthenticationAsset : CustomAsset.OfType<Authentication.User> {
    /// <summary>
    /// Name of the logged in player (often email) - defaults to guest.
    /// </summary>
    public string Name { get { return Value.Name; } set { Set(() => Value.Name = value); } }

    /// <summary>
    /// Email address of the logged in player - defaults to empty.
    /// </summary>
    public string Email { get { return Value.Email; } set { Set(() => Value.Email = value); } }
  }
}