using System;
using System.Collections;
using UnityEngine;

namespace Decoupled {
  /// <inheritdoc />
  /// <summary>
  /// Interface to a device gyroscope.
  /// </summary>
  [Serializable]
  public class Gyro : Service<Gyro> {
    [SerializeField, Tooltip("larger for more stability, smaller for faster following")]
    private float minimumChange = 0.01f;

    /// <summary>
    /// Amount of time between gyroscope checks (in seconds
    /// </summary>
    protected WaitForSecondsRealtime PollingInterval;

    /// <summary>
    /// Used to compare to see if we have a change to report
    /// </summary>
    protected Quaternion LastReading;

    /// <summary>
    /// Coroutine that checks for changes to coordinates at set intervals. This will trigger an event for any who are listening.
    /// </summary>
    public IEnumerator StartPolling() {
      while (!Offline) {
        float change = Mathf.Abs(Quaternion.Dot(Attitude, LastReading)) - 1;

        if (change > minimumChange) {
          LastReading = Attitude;
          Changed();
        }

        yield return PollingInterval;
      }
    }

    /// <summary>
    /// Start a coroutine to poll the GPS on the given MonoBehaviour.
    /// </summary>
    /// <param name="monoBehaviour">The MonoBehaviour that owns the polling coroutine</param>
    public void StartPolling(MonoBehaviour monoBehaviour) {
      monoBehaviour.StartCoroutine(StartPolling());
    }

    /// <summary>
    /// Called on change so we can act on it
    /// </summary>
    protected virtual void Changed() { }

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

    /// <summary>
    ///   <para>Sets or retrieves gyroscope interval in seconds.</para>
    /// </summary>
    public virtual float UpdateInterval {
      get { return 1; }
      set { PollingInterval = new WaitForSecondsRealtime(value); }
    }
  }
}