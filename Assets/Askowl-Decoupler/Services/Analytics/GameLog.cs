using UnityEngine;

namespace Decoupled.Analytics {
  using JetBrains.Annotations;

  public sealed class GameLog : Service<GameLog> {
    [UsedImplicitly]
    public void Screen(string name, string clazz) {
      Debug.Log(message: "**** Screen '" + name + "' - " + clazz);
    }

    [UsedImplicitly]
    public void AppOpen() { Debug.Log(message: "**** App Open"); }

    [UsedImplicitly]
    public void EarnVirtualCurrency(object value, string currency) {
      Debug.Log(message: "**** Earn Virtual Currency " + value + " " + currency);
    }

    // ReSharper disable once MemberCanBeMadeStatic.Global
    public void Error(string message) { Debug.Log(message: "**** Error '" + message + "'"); }

    [NotNull]
    public string More([NotNull] params object[] list) {
      return string.Join(separator: ",",
                         value: System.Array.ConvertAll(array: list, converter: x => x.ToString()));
    }

    // ReSharper disable once MemberCanBeMadeStatic.Global
    public void Event(string name, string action, string result, [CanBeNull] string csv = null) {
      Debug.Log("**** Event '" + name + "' -- action: " + action + ", result: " + result +
                ", more: "     + csv);
    }

    [UsedImplicitly]
    public void JoinGroup(string groupId) {
      Debug.Log(message: "**** Join Group '" + groupId + "'");
    }

    [UsedImplicitly]
    public void LevelUp(object level, [CanBeNull] string character = null) {
      Debug.Log(message: "**** Level Up to '" + level + "' for '" + character + "'");
    }

    [UsedImplicitly]
    public void Login() { Debug.Log(message: "**** Login"); }

    [UsedImplicitly]
    public void PostScore(int score, object level = null, string character = null) {
      Debug.Log(message: "**** Post Score " + score + ", level " + level + ", character '" +
                         character          + "'");
    }

    [UsedImplicitly]
    public void SelectContent(string contentType, string itemId) {
      Debug.Log(message: "**** Select Content '" + itemId + "', type '" + contentType + "'");
    }

    [UsedImplicitly]
    public void Share(string contentType, string itemId) {
      Debug.Log(message: "**** Share '" + itemId + "', type '" + contentType + "'");
    }

    [UsedImplicitly]
    public void SignUp() { Debug.Log(message: "**** Sign Up"); }

    [UsedImplicitly]
    public void SpendVirtualCurrency(object value, string currency, string item) {
      Debug.Log(message: "**** Spend Virtual Currency " + value + " " + currency + " on '" + item +
                         "'");
    }

    [UsedImplicitly]
    public void TutorialBegin() { Debug.Log(message: "**** Tutorial Begin"); }

    [UsedImplicitly]
    public void TutorialComplete() { Debug.Log(message: "**** Tutorial Complete"); }

    [UsedImplicitly]
    public void UnlockAchievement(string achievementId) {
      Debug.Log(message: "**** Unlock Achievement");
    }

    [UsedImplicitly]
    public void ViewSearchResults(string searchTerm) {
      Debug.Log(message: "**** View Search Results");
    }
  }
}