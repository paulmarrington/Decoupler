namespace Askowl {
  using Decoupled;
  using JetBrains.Annotations;
  using UnityEngine;

  public sealed class AnalyticsUnity : Analytics {
    public override void Event(string                    name,
                               string                    action,
                               string                    result,
                               [NotNull] params object[] more) {
      UnityEngine.Analytics.Analytics.CustomEvent(name, ToDictionary(action, result, more));
    }

//
//    public override string Gender {
//      set {
//        switch (value) {
//          case "Male":
//            UnityEngine.Analytics.Analytics.SetUserGender(UnityEngine.Analytics.Gender.Male);
//            break;
//          case "Female":
//            UnityEngine.Analytics.Analytics.SetUserGender(UnityEngine.Analytics.Gender.Female);
//            break;
//          default:
//            UnityEngine.Analytics.Analytics.SetUserGender(UnityEngine.Analytics.Gender.Unknown);
//            break;
//        }
//      }
//    }
//
//
//    public override int BirthYear {
//      set { UnityEngine.Analytics.Analytics.SetUserBirthYear(value); }
//    }
//
//    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
//    private static void RegisterService() { Register<AnalyticsUnity>(); }
  }
}