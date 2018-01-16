using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decoupled.Analytics {
  public class Play : Decoupled.Service<Play> {

    public virtual void Screen(string name, string clazz) {
    }

    public virtual void AppOpen() {
    }

    public virtual void EarnVirtualCurrency(object value, string currency) {
    }

    public virtual void Error(string message) {
    }

    public virtual void Event(string name, params object[] nvp) {
    }

    public virtual void JoinGroup(string groupId) {
    }

    public virtual void LevelUp(object level, string character = null) {
    }

    public virtual void Login() {
    }

    public virtual void PostScore(int score, object level = null, string character = null) {
    }

    public virtual void SelectContent(string contentType, string itemId) {
    }

    public virtual void Share(string contentType, string itemId) {
    }

    public virtual void SignUp() {
    }

    public virtual void SpendVirtualCurrency(object value, string currency, string item) {
    }

    public virtual void TutorialBegin() {
    }

    public virtual void TutorialComplete() {
    }

    public virtual void UnlockAchievement(string achievementId) {
    }

    public virtual void ViewSearchResults(string searchTerm) {
    }
  }
}