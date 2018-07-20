using System;
using Askowl;
using UnityEngine;

namespace Decoupled {
  // ReSharper disable once InconsistentNaming
  /// <inheritdoc />
  /// Decoupled interface to device GPS
  [Serializable]
  public class GPSService : Service<GPSService> {
    // ReSharper disable Unity.RedundantSerializeFieldAttribute

    [SerializeField] private float desiredAccuracyInMeters = 1;
    [SerializeField] private float updateDistanceInMeters  = 0.25f;

    // ReSharper restore Unity.RedundantSerializeFieldAttribute

    /// <summary>
    /// Device independent location storage
    /// </summary>
    public struct LocationData {
      public float  Latitude;
      public float  Longitude;
      public float  AltitudeInMeters;
      public double Timestamp;
      public float  VerticalAccuracyInMetres;
      public float  HorizontalAccuracyInMetres;

      public bool IsSet => Timestamp > 0;

      public override string ToString() =>
        $"{Longitude:n5}, {Latitude:n5}, alt: {AltitudeInMeters:n2}";
    }

    private LocationData location, lastLocation;

    public LocationData Location { get { return location; } protected set { location = value; } }

    /// <summary>
    /// Set in Unity inspector, but GPS really controls accuracy
    /// </summary>
    public float DesiredAccuracyInMeters => desiredAccuracyInMeters;

    /// <summary>
    /// Set int he Unity inspector, but GPS decides whether to take notice
    /// </summary>
    public float UpdateDistanceInMeters => updateDistanceInMeters;

    /// <summary>
    /// Set true if the code is running on a device with GPS, the user has enabled GPS access and we have started the GPS tracking.
    /// </summary>
    public virtual bool Running => false;

    /// <summary>
    /// Set true if we do not have access to the GPS or its initialisation failed
    /// </summary>
    public virtual bool Offline => true;

    /// <summary>
    /// If all is good start the GPS tracking position. Remember that this eats battery.
    /// </summary>
    public virtual void StartTracking() { }

    /// Copy from device location structure into the device independent one.
    /// Translate where needed - especially timestamp
    /// <returns>true if location has changed</returns>
    public bool UpdateLocation() {
      lastLocation = location;
      location     = ReadLocation();

      float vAccuracy = location.VerticalAccuracyInMetres   / 2;
      float hAccuracy = location.HorizontalAccuracyInMetres * 1e-6f;

      return Changed = !Compare.AlmostEqual(Latitude,  lastLocation.Latitude,         hAccuracy) ||
                       !Compare.AlmostEqual(Longitude, lastLocation.Longitude,        hAccuracy) ||
                       !Compare.AlmostEqual(Altitude,  lastLocation.AltitudeInMeters, vAccuracy);
    }

    protected virtual LocationData ReadLocation() {
      Debug.LogError("Must implement 'ReadLocation'");
      return new LocationData();
    }

    /// <summary>
    /// Continue to return false until the GPS comes on-line
    /// </summary>
    public virtual bool Initialising => false;

    /// <summary>
    /// Turns off GPS tracking - saving battery.
    /// </summary>
    public virtual void StopTracking() { }

    /// <summary>
    /// Latitude of the last GPS read
    /// </summary>
    public float Latitude => location.Latitude;

    /// <summary>
    /// Longitude of the last GPS read
    /// </summary>
    public float Longitude => location.Longitude;

    /// <summary>
    /// Altitude of the last GPS read
    /// </summary>
    public float Altitude => location.AltitudeInMeters;

    /// <summary>
    /// Timestamp of the last GPS read
    /// </summary>
    public double Timestamp => location.Timestamp;

    /// <summary>
    /// Horizontal accuracy in metres
    /// </summary>
    public float HorizontalAccuracy => location.HorizontalAccuracyInMetres;

    /// <summary>
    /// Vertical accuracy in metres
    /// </summary>
    public float VerticalAccuracy => location.VerticalAccuracyInMetres;

    /// Fetch the current device coordinates and see if they have changed from last time.
    public bool Changed { get; private set; }

    /// <inheritdoc />
    public override bool Equals(object other) => !Changed;

    /// <inheritdoc />
    // ReSharper disable once NonReadonlyMemberInGetHashCode
    public override int GetHashCode() => location.GetHashCode();

    /// <inheritdoc/>
    public override string ToString() => location.ToString();
  }
}