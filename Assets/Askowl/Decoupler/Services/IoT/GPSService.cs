namespace Decoupled {
  using System;
  using Askowl;
  using UnityEngine;

  /// <a href=""></a> //#TBD#// <inheritdoc />
  [Serializable] public class GpsService : Service<GpsService> {
    [SerializeField, Tooltip("Set in Unity inspector, but GPS really controls accuracy")]
    private float desiredAccuracyInMeters = 1;

    [SerializeField, Tooltip("Set in Unity inspector, but GPS really controls accuracy")]
    private float updateDistanceInMeters = 0.25f;

    /// <a href="">Device independent location storage</a> //#TBD#//
    public struct LocationData {
      // ReSharper disable MissingXmlDoc
      public float  Latitude;
      public float  Longitude;
      public float  AltitudeInMeters;
      public double Timestamp;
      public float  VerticalAccuracyInMetres;
      public float  HorizontalAccuracyInMetres;

      public bool IsSet => Timestamp > 0;

      public override string ToString() => $"{Longitude:n5}, {Latitude:n5}, alt: {AltitudeInMeters:n2}";
    }
    // ReSharper restore MissingXmlDoc

    private LocationData location, lastLocation;

    /// <a href="">The last location the GPS device recorded</a> //#TBD#//
    public LocationData Location { get => location; protected set => location = value; }

    /// <a href="">Set in Unity inspector, but GPS really controls accuracy</a> //#TBD#//
    public float DesiredAccuracyInMeters => desiredAccuracyInMeters;

    /// <a href="">Set int he Unity inspector, but GPS decides whether to take notice</a> //#TBD#//
    public float UpdateDistanceInMeters => updateDistanceInMeters;

    /// <a href="">Set true if the code is running on a device with GPS, the user has enabled GPS access and we have started the GPS tracking</a> //#TBD#//
    public virtual bool Running => false;

    /// <a href="">Set true if we do not have access to the GPS or its initialisation failed</a> //#TBD#//
    public virtual bool Offline => true;

    /// <a href="">If all is good start the GPS tracking position. Remember that this eats battery</a> //#TBD#//
    public virtual void StartTracking() { }

    /// <a href="">Copy from device location structure into the device independent one</a> //#TBD#//
    public bool UpdateLocation() {
      lastLocation = location;
      location     = ReadLocation();

      float vAccuracy = location.VerticalAccuracyInMetres   / 2;
      float hAccuracy = location.HorizontalAccuracyInMetres * 1e-6f;

      return Changed = !Compare.AlmostEqual(Latitude,  lastLocation.Latitude,         hAccuracy) ||
                       !Compare.AlmostEqual(Longitude, lastLocation.Longitude,        hAccuracy) ||
                       !Compare.AlmostEqual(Altitude,  lastLocation.AltitudeInMeters, vAccuracy);
    }

    /// <a href="">Poll the GPS device to get the latest location data</a> //#TBD#//
    protected virtual LocationData ReadLocation() {
      Debug.LogError("Must implement 'ReadLocation'");
      return new LocationData();
    }

    /// <a href="">Continue to return false until the GPS comes on-line</a> //#TBD#//
    public virtual bool Initialising => false;

    /// <a href="">Turns off GPS tracking - saving battery</a> //#TBD#//
    public virtual void StopTracking() { }

    /// <a href="">Latitude of the last GPS read</a> //#TBD#//
    public float Latitude => location.Latitude;

    /// <a href="">Longitude of the last GPS read</a> //#TBD#//
    public float Longitude => location.Longitude;

    /// <a href="">Altitude of the last GPS read</a> //#TBD#//
    public float Altitude => location.AltitudeInMeters;

    /// <a href="">Timestamp of the last GPS read</a> //#TBD#//
    public double Timestamp => location.Timestamp;

    /// <a href="">Horizontal accuracy in metres</a> //#TBD#//
    public float HorizontalAccuracy => location.HorizontalAccuracyInMetres;

    /// <a href="">Vertical accuracy in metres</a> //#TBD#//
    public float VerticalAccuracy => location.VerticalAccuracyInMetres;

    /// <a href="">Fetch the current device coordinates and see if they have changed from last time</a> //#TBD#//
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