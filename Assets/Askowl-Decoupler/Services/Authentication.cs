using System.Collections;
using System;

namespace Decoupled {
  using JetBrains.Annotations;

  [UsedImplicitly]
  public sealed class Authentication : Service<Authentication> {
    private sealed class User {
      // ReSharper disable NotAccessedField.Global
      // ReSharper disable NotAccessedField.Local
      internal string Name        = "guest";
      internal string Email       = "";
      internal string PhotoUrl    = "";
      internal string PhoneNumber = "";
      internal string ProviderId  = "";
      internal string UserId      = "";
      internal bool   IsVerified  = false;
      internal bool   IsLoggedIn  = false;

      internal object MetaData = null;
      // ReSharper restore NotAccessedField.Local
      // ReSharper restore NotAccessedField.Global
    }

    private readonly User user = new User();

    [UsedImplicitly]
    public IEnumerator CreateUser(string                     email, string password,
                                  [CanBeNull] Action<string> error = null) {
      user.Name = user.Email = email;
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator UpdateProfile(string                     displayName,
                                     [CanBeNull] string         photoUrl = null,
                                     [CanBeNull] Action<string> error    = null) {
      user.Name     = displayName;
      user.PhotoUrl = photoUrl;
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator SignIn(string                     email, string password,
                              [CanBeNull] Action<string> error = null) {
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator SignIn(object credential, [CanBeNull] Action<string> error = null) {
      yield return null;
    }

    [UsedImplicitly]
    public void SignOut() { }

    [UsedImplicitly]
    public IEnumerator Anonymous([CanBeNull] Action<string> error = null) { yield return null; }

    [UsedImplicitly]
    public IEnumerator LinkWith(object credential, [CanBeNull] Action<string> error = null) {
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator Reload([CanBeNull] Action<string> error = null) { yield return null; }

    [UsedImplicitly]
    public IEnumerator GetToken(Action<string> setToken, [CanBeNull] Action<string> error = null) {
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator DeleteUser([CanBeNull] Action<string> error = null) { yield return null; }

    [UsedImplicitly]
    public IEnumerator ProvidersFor(string                     email, Action<string[]> providers,
                                    [CanBeNull] Action<string> error = null) {
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator PasswordReset(string email, [CanBeNull] Action<string> error = null) {
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator VerifyPhoneNumber(string                     number, Action<string> setId,
                                         [CanBeNull] Action<string> error = null) {
      yield return null;
    }

    [UsedImplicitly]
    public IEnumerator PhoneSignIn(string                     phoneAuthVerificationId,
                                   string                     receivedCode,
                                   [CanBeNull] Action<string> error = null) {
      yield return null;
    }
  }
}