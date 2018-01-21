using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Decoupled {
  public class Authentication : Decoupled.Service<Authentication> {

    public class User {
      public string Name = "guest";
      public string Email = "";
      public string PhotoUrl = "";
      public string PhoneNumber = "";
      public string ProviderId = "";
      public string UserId = "";
      public bool IsVerified = false;
      public bool IsLoggedIn = false;
      public object MetaData = null;
    }

    public User user = new User ();

    public virtual IEnumerator CreateUser(string email, string password, Action<string> error = null) {
      user.Name = user.Email = email;
      yield return null;
    }

    public virtual IEnumerator UpdateProfile(string displayName, string photoUrl = null, Action<string> error = null) {
      user.Name = displayName;
      user.PhotoUrl = photoUrl;
      yield return null;
    }

    public virtual IEnumerator SignIn(string email, string password, Action<string> error = null) {
      yield return null;
    }

    public virtual IEnumerator SignIn(object credential, Action<string> error = null) {
      yield return null;
    }

    public virtual void SignOut() {
    }

    public virtual IEnumerator Anonymous(Action<string> error = null) {
      yield return null;
    }

    public virtual IEnumerator LinkWith(object credential, Action<string> error = null) {
      yield return null;
    }

    public virtual IEnumerator Reload(Action<string> error = null) {
      yield return null;
    }

    public virtual IEnumerator GetToken(Action<string> setToken, Action<string> error = null) {
      yield return null;
    }

    public virtual IEnumerator DeleteUser(Action<string> error = null) {
      yield return null;
    }

    public virtual IEnumerator ProvidersFor(string email, Action<string[]> providers, Action<string> error = null) {
      yield return null;
    }

    public virtual IEnumerator PasswordReset(string email, Action<string> error = null) {
      yield return null;
    }

    public virtual IEnumerator VerifyPhoneNumber(string number, Action<string> setId, Action<string> error = null) {
      yield return null;
    }

    public virtual IEnumerator PhoneSignIn(string phoneAuthVerificationId,
                                           string receivedCode,
                                           Action<string> error = null) {
      yield return null;
    }
  }
}