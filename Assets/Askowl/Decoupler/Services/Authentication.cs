// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

//using CustomAsset.Mutable;

namespace Decoupled {
  using System;
  using System.Collections;

  /// <a href=""></a> //#TBD#// <inheritdoc />
  public sealed class Authentication : Service<Authentication> {
//    private AuthenticationAsset user = AuthenticationAsset.Instance("Guest");

    /// <a href=""></a> //#TBD#//
    public IEnumerator CreateUser(
      string         email, string password,
      Action<string> error = null) {
//      user      = AuthenticationAsset.Instance(email);
//      user.Name = user.Email = email;
      yield return null;
    }

    /// <a href=""></a> //#TBD#//
    public IEnumerator UpdateProfile(string displayName, Action<string> error = null) {
//      user.Name = displayName;
      yield return null;
    }

    /// <a href=""></a> //#TBD#//
    public IEnumerator SignIn(string email, string password, Action<string> error = null) { yield return null; }

    /// <a href=""></a> //#TBD#//
    public IEnumerator SignIn(object credential, Action<string> error = null) { yield return null; }

    /// <a href=""></a> //#TBD#//
    public void SignOut() {
//      Object.Destroy(user);
//      user = AuthenticationAsset.Instance("Guest");
    }

    /// <a href=""></a> //#TBD#//
    public IEnumerator Anonymous(Action<string> error = null) { yield return null; }

    /// <a href=""></a> //#TBD#//
    public IEnumerator LinkWith(object credential, Action<string> error = null) { yield return null; }

    /// <a href=""></a> //#TBD#//
    public IEnumerator Reload(Action<string> error = null) { yield return null; }

    /// <a href=""></a> //#TBD#//
    public IEnumerator GetToken(Action<string> setToken, Action<string> error = null) { yield return null; }

    /// <a href=""></a> //#TBD#//
    public IEnumerator DeleteUser(Action<string> error = null) { yield return null; }

    /// <a href=""></a> //#TBD#//
    public IEnumerator ProvidersFor(
      string           email,
      Action<string[]> providers,
      Action<string>   error = null) {
      yield return null;
    }

    /// <a href=""></a> //#TBD#//
    public IEnumerator PasswordReset(string email, Action<string> error = null) { yield return null; }

    /// <a href=""></a> //#TBD#//
    public IEnumerator VerifyPhoneNumber(
      string         number,
      Action<string> setId,
      Action<string> error = null) {
      yield return null;
    }

    /// <a href=""></a> //#TBD#//
    public IEnumerator PhoneSignIn(
      string         phoneAuthVerificationId,
      string         receivedCode,
      Action<string> error = null) {
      yield return null;
    }

    /// <a href=""></a> //#TBD#//
    public class User {
      #pragma warning disable 414
      internal          string Name        = "guest";
      internal readonly string Email       = "";
      internal          string PhotoUrl    = "";
      internal          string PhoneNumber = "";
      internal          string ProviderId  = "";
      internal          string UserId      = "";
      internal          string Gender      = "Unknown";
      internal          bool   IsVerified  = false;
      internal          bool   IsLoggedIn  = false;
      internal          int    BirthYear   = 0;
      internal          object MetaData    = null;
      #pragma warning restore 414

      /// <a href=""></a> //#TBD#//
      public override bool Equals(object other) {
        var one = other as User;
        return (one != null) && string.Equals(Email, one.Email);
      }

      /// <a href=""></a> //#TBD#//
      public override int GetHashCode() => (Email != null ? Email.GetHashCode() : 0);
    }

//
//    public class AuthenticationAsset : OfType<User> {
//      public static AuthenticationAsset Instance(string name) {
//        return Instance<AuthenticationAsset>(name);
//      }
//
//      /// <summary>
//      /// Name of the logged in player (often email) - defaults to guest.
//      /// </summary>
//      public string Name { get { return Value.Name; } set { this.Set(ref Value.Name, value); } }
//
//      /// <summary>
//      /// Email address of the logged in player - defaults to empty.
//      /// </summary>
//      public string Email { get { return Value.Email; } set { this.Set(ref Value.Email, value); } }
//    }
  }
}