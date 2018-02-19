using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decoupled.Analytics {
  public class GameLog : Decoupled.Service<GameLog> {

    public virtual void Screen(string name, string clazz) {
      Debug.Log("**** Screen '" + name + "' - " + clazz);
    }

    public virtual void AppOpen() {
      Debug.Log("**** App Open");
    }

    public virtual void EarnVirtualCurrency(object value, string currency) {
      Debug.Log("**** Earn Virtual Currency " + value + " " + currency);
    }

    public virtual void Error(string message) {
      Debug.Log("**** Error '" + message + "'");
    }

    public virtual void Event(string name, params object[] nvp) {
      Debug.Log("**** Event '" + name + "'");
    }

    public virtual void JoinGroup(string groupId) {
      Debug.Log("**** Join Group '" + groupId + "'");
    }

    public virtual void LevelUp(object level, string character = null) {
      Debug.Log("**** Level Up to '" + level + "' for '" + character + "'");
    }

    public virtual void Login() {
      Debug.Log("**** Login");
    }

    public virtual void PostScore(int score, object level = null, string character = null) {
      Debug.Log("**** Post Score " + score + ", level " + level + ", character '" + character + "'");
    }

    public virtual void SelectContent(string contentType, string itemId) {
      Debug.Log("**** Select Content '" + itemId + "', type '" + contentType + "'");
    }

    public virtual void Share(string contentType, string itemId) {
      Debug.Log("**** Share '" + itemId + "', type '" + contentType + "'");
    }

    public virtual void SignUp() {
      Debug.Log("**** Sign Up");
    }

    public virtual void SpendVirtualCurrency(object value, string currency, string item) {
      Debug.Log("**** Spend Virtual Currency " + value + " " + currency + " on '" + item + "'");
    }

    public virtual void TutorialBegin() {
      Debug.Log("**** Tutorial Begin");
    }

    public virtual void TutorialComplete() {
      Debug.Log("**** Tutorial Complete");
    }

    public virtual void UnlockAchievement(string achievementId) {
      Debug.Log("**** Unlock Achievement");
    }

    public virtual void ViewSearchResults(string searchTerm) {
      Debug.Log("**** View Search Results");
    }
  }
}