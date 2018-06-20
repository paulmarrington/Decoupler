using System;
using UnityEngine;

namespace Askowl {
  /// <inheritdoc />
  /// <summary>
  /// GPS Custom Asset
  /// </summary>
  [CreateAssetMenu(menuName = "Custom Assets/GPS")]
  // ReSharper disable once InconsistentNaming
  public class GPS : CustomAsset.Mutable.OfType<Decoupled.GPS> {
    /// <summary>
    /// Different name for Value
    /// </summary>
    public Decoupled.GPS Device;

    private Decoupled.GPS.LocationData lastLocation = new Decoupled.GPS.LocationData();

    /// <inheritdoc />
    protected override void OnEnable() {
      base.OnEnable();
      Device = Value = Decoupled.GPS.Instance;
    }

    protected void Changed() {
      lastLocation = Value.Location;
      Emitter.Fire();
    }

    /// <inheritdoc />
    protected override bool Equals(Decoupled.GPS other) {
      Value.UpdateLocation();
      Decoupled.GPS.LocationData now = other.Location;

      return ((Math.Abs(now.Latitude         - lastLocation.Latitude)         > 0.000005f) ||
              (Math.Abs(now.Longitude        - lastLocation.Longitude)        > 0.000005f) ||
              (Math.Abs(now.AltitudeInMeters - lastLocation.AltitudeInMeters) > 0.25f));
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
    [SerializeField] private float desiredAccuracyInMeters = 1;
    [SerializeField] private float updateDistanceInMeters  = 0.25f;

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

    protected internal LocationData Location = new LocationData();

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
    /// Copy from device location structure into the device independent one.
    /// Translate where needed - especially timestamp
    /// </summary>
    public virtual void UpdateLocation() { }

    /// <summary>
    /// Continue to return false until the GPS comes on-line
    /// </summary>
    public virtual bool Initialising { get { return false; } }

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