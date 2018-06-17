using System;
using System.Collections;
using UnityEngine;

namespace Askowl {
  [CreateAssetMenu(menuName = "Custom Assets/Gyroscope")]
  public class GPS : CustomAsset.OfType<Decoupled.GPS> {
    [SerializeField, Tooltip("larger for more stability, smaller for faster following")]
    private float minimumChange = 0.01f;

    [SerializeField] private float updateIntervalInSeconds = 0;

    /// <summary>
    /// Set if Gyro failed to initialise
    /// </summary>
    public bool Offline { get { return Value.Offline; } }

    /// <summary>
    /// Amount of time between gyroscope checks (in seconds
    /// </summary>
    protected WaitForSecondsRealtime PollingInterval;

    protected virtual void OnEnable() {
      base.OnEnable();
      Value          = Decoupled.GPS.Instance;
      UpdateInterval = updateIntervalInSeconds;
    }


    /// <summary>
    /// Start a coroutine to poll the gyroscope on the given MonoBehaviour.
    /// </summary>
    /// <param name="monoBehaviour">The MonoBehaviour that owns the polling coroutine</param>
    public virtual void Start(MonoBehaviour monoBehaviour) {
      if (!Offline) monoBehaviour.StartCoroutine(StartPolling());
    }

    protected override bool Equals(Decoupled.GPS other) {
      Decoupled.GPS.LocationData before = other.LastLocation, now = Value.Location;

      return ((Math.Abs(now.Latitude         - before.Latitude)         > 0.000005f) ||
              (Math.Abs(now.Longitude        - before.Longitude)        > 0.000005f) ||
              (Math.Abs(now.AltitudeInMeters - before.AltitudeInMeters) > 0.25f));
    }
  }
}

namespace Decoupled {
  // ReSharper disable once InconsistentNaming
  /// <inheritdoc />
  /// <summary>
  /// Decoupled interface to device GPS
  /// </summary>
  [Serializable]
  public class GPS : Service<GPS> {
    [SerializeField] private float desiredAccuracyInMeters  = 1;
    [SerializeField] private float updateDistanceInMeters   = 0.25f;
    [SerializeField] private float pollingIntervalInSeconds = 1;

    /// <summary>
    /// Device independant location storage
    /// </summary>
    public struct LocationData {
      internal float  Latitude;
      internal float  Longitude;
      internal float  AltitudeInMeters;
      internal double Timestamp;
      internal float  VerticalAccuracyInMetres;
      internal float  HorizontalAccuracyInMetres;
    }

    /// <summary>
    /// Set in Unity inspector, but GPS really controls accuracy
    /// </summary>
    public float DesiredAccuracyInMeters { get { return desiredAccuracyInMeters; } }

    /// <summary>
    /// Set int he Unity inspector, but GPS decides whether to take notice
    /// </summary>
    public float UpdateDistanceInMeters { get { return updateDistanceInMeters; } }

    /// <summary>
    /// Set in Unity inspector. How often do we read the GPS. No point in exceeding the GPS ability.
    /// </summary>
    public float PollingIntervalInSeconds { get { return pollingIntervalInSeconds; } }

    public LocationData LastLocation { get { return lastLocation; } }

    protected internal WaitForSecondsRealtime PollingInterval;
    protected internal LocationData           Location = new LocationData();

    private LocationData lastLocation = new LocationData();

    /// <summary>
    /// Set true if the code is running on a device with GPS, the user has enabled GPS access and we have started the GPS tracking.
    /// </summary>
    public virtual bool Running { get { return false; } }

    /// <summary>
    /// Set true if we do not have access to the GPS or its initialisation failed
    /// </summary>
    public virtual bool Offline { get { return true; } }

    /// <summary>
    /// If all is good start the GPS tracking position. Remember that this eats battery.
    /// </summary>
    public virtual void StartTracking() { }

    /// <summary>
    /// Coroutine that checks for changes to coordinates at set intervals. This will trigger an event for any who are listening.
    /// </summary>
    public IEnumerator StartPolling() {
      if (Offline) yield break;

      while (Initialising) yield return PollingInterval;

      while (!Offline) {
        UpdateLocation();

        if ((Math.Abs(Location.Latitude         - lastLocation.Latitude)         > 0.000005f) ||
            (Math.Abs(Location.Longitude        - lastLocation.Longitude)        > 0.000005f) ||
            (Math.Abs(Location.AltitudeInMeters - lastLocation.AltitudeInMeters) > 0.25f)) {
          lastLocation = Location;
        }

        yield return PollingInterval;
      }
    }

    /// <summary>
    /// Copy from device location structure into the device independent one.
    /// Translate where needed - especially timestamp
    /// </summary>
    protected virtual void UpdateLocation() { }

    /// <summary>
    /// Continue to return false until the GPS comes on-line
    /// </summary>
    public virtual bool Initialising { get { return false; } }

    /// <summary>
    /// Start a coroutine to poll the GPS on the given MonoBehaviour.
    /// </summary>
    /// <param name="monoBehaviour">The MonoBehaviour that owns the polling coroutine</param>
    public virtual void Start(MonoBehaviour monoBehaviour) {
      if (!Offline) monoBehaviour.StartCoroutine(StartPolling());
    }

    /// <summary>
    /// Turns off GPS tracking - saving battery.
    /// </summary>
    public virtual void StopTracking() { }

    /// <summary>
    /// Latitude of the last GPS read
    /// </summary>
    public float Latitude { get { return Location.Latitude; } }

    /// <summary>
    /// Longitude of the last GPS read
    /// </summary>
    public float Longitude { get { return Location.Longitude; } }

    /// <summary>
    /// Altitude of the last GPS read
    /// </summary>
    public float Altitude { get { return Location.AltitudeInMeters; } }

    /// <summary>
    /// Timestamp of the last GPS read
    /// </summary>
    public double Timestamp { get { return Location.Timestamp; } }

    /// <summary>
    /// Horizontal accuracy in metres
    /// </summary>
    public float HorizontalAccuracy { get { return Location.HorizontalAccuracyInMetres; } }

    /// <summary>
    /// Vertical accuracy in metres
    /// </summary>
    public float VerticalAccuracy { get { return Location.VerticalAccuracyInMetres; } }
  }
}