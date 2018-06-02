using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Decoupled {
  public class Gyro : Service<Gyro> {
    [SerializeField, Tooltip("larger for more stability, smaller for faster following")]
    private float minimumChange = 0.01f;

    protected WaitForSecondsRealtime pollingInterval;
    protected Quaternion             lastReading;

    /// <summary>
    /// Coroutine that checks for changes to coordinates at set intervals. This will trigger an event for any who are listening.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public IEnumerator StartPolling() {
      while (!Failed) {
        float change = Mathf.Abs(Quaternion.Dot(Attitude, lastReading)) - 1;

        if (change > minimumChange) {
          lastReading = Attitude;
          Changed();
        }

        yield return pollingInterval;
      }
    }

    /// <summary>
    /// Start a coroutine to poll the GPS on the given MonoBehaviour.
    /// </summary>
    /// <param name="monoBehaviour">The MonoBehaviour that owns the polling coroutine</param>
    public void StartPolling(MonoBehaviour monoBehaviour) {
      monoBehaviour.StartCoroutine(StartPolling());
    }

    protected virtual void Changed() { }

    public bool Failed { get { return true; } }

    public bool Ready { get { return false; } }

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
    public virtual bool Enabled { get { return false; } set { } }

    /// <summary>
    ///   <para>Sets or retrieves gyroscope interval in seconds.</para>
    /// </summary>
    public virtual float UpdateInterval { get { return 1; } set { } }
  }
}