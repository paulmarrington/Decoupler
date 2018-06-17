using System;
using System.Collections;
using UnityEngine;

namespace Askowl {
  [CreateAssetMenu(menuName = "Custom Assets/Gyroscope")]
  public class Gyro : CustomAsset.OfType<Decoupled.Gyro> {
    [SerializeField, Tooltip("larger for more stability, smaller for faster following")]
    private float minimumChange = 0.01f;

    [SerializeField] private float updateIntervalInSeconds = 1;

    /// <summary>
    ///   <para>Sets or retrieves gyroscope interval in seconds.</para>
    /// </summary>
    public virtual float UpdateInterval {
      get { return updateIntervalInSeconds; }
      set { PollingInterval = new WaitForSecondsRealtime(updateIntervalInSeconds = value); }
    }

    /// <summary>
    /// Set if Gyro failed to initialise
    /// </summary>
    public bool Offline { get { return Value.Offline; } }

    /// <summary>
    ///   <para>Returns the attitude (ie, orientation in space) of the device.</para>
    /// </summary>
    public Quaternion Attitude { get { return Value.Attitude; } }

    /// <summary>
    /// Amount of time between gyroscope checks (in seconds
    /// </summary>
    protected WaitForSecondsRealtime PollingInterval;

    protected virtual void OnEnable() {
      base.OnEnable();
      Value          = Decoupled.Gyro.Instance;
      UpdateInterval = updateIntervalInSeconds;
    }

    /// <summary>
    /// Coroutine that checks for changes to coordinates at set intervals. This will trigger an event for any who are listening.
    /// </summary>
    public IEnumerator StartPolling() {
      while (!Offline) {
        if (!Equals(Value)) Changed();

        yield return PollingInterval;
      }
    }

    /// <summary>
    /// Start a coroutine to poll the gyroscope on the given MonoBehaviour.
    /// </summary>
    /// <param name="monoBehaviour">The MonoBehaviour that owns the polling coroutine</param>
    public virtual void Start(MonoBehaviour monoBehaviour) {
      if (!Offline) monoBehaviour.StartCoroutine(StartPolling());
    }

    protected override bool Equals(Decoupled.Gyro other) {
      float change = Mathf.Abs(Quaternion.Dot(Value.Attitude, other.LastReading)) - 1;
      return (change <= minimumChange);
    }
  }
}

namespace Decoupled {
  /// <inheritdoc />
  /// <summary>
  /// Interface to a device gyroscope.
  /// </summary>
//  [Serializable]
  public class Gyro : Service<Gyro> {
    /// <summary>
    /// Used to compare to see if we have a change to report
    /// </summary>
    public Quaternion LastReading;

    /// <inheritdoc />
    /// <summary>
    /// Call in implementation constructor
    /// </summary>
    protected override void Initialise() {
      if (Offline) return;

      Enabled = true;
    }

    /// <summary>
    /// Set if Gyro failed to initialise
    /// </summary>
    public virtual bool Offline { get { return true; } }

    /// <summary>
    ///   <para>Returns rotation rate as measured by the device's gyroscope.</para>
    /// </summary>
    public virtual Vector3 RotationRate { get { return Vector3.zero; } }

    /// <summary>
    ///   <para>Returns unbiased rotation rate as measured by the device's gyroscope.</para>
    /// </summary>
    public virtual Vector3 RotationRateUnbiased { get { return Vector3.zero; } }

    /// <summary>
    ///   <para>Returns the gravity acceleration vector expressed in the device's reference frame.</para>
    /// </summary>
    public virtual Vector3 Gravity { get { return Vector3.zero; } }

    /// <summary>
    ///   <para>Returns the acceleration that the user is giving to the device.</para>
    /// </summary>
    public virtual Vector3 UserAcceleration { get { return Vector3.zero; } }

    /// <summary>
    ///   <para>Returns the attitude (ie, orientation in space) of the device.</para>
    /// </summary>
    public virtual Quaternion Attitude { get { return Quaternion.identity; } }

    /// <summary>
    ///   <para>Sets or retrieves the enabled status of this gyroscope.</para>
    /// </summary>
    public virtual bool Enabled { get; set; }
  }
}