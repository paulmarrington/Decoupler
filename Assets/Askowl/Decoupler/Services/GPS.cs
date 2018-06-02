using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Decoupled {
  public class GPS : Service<GPS> {
    protected class LocationData {
      internal float  Latitude;
      internal float  Longitude;
      internal float  AltitudeInMeters;
      internal double Timestamp;
      internal float  VerticalAccuracyInMetres;
      internal float  HorizontalAccuracyInMetres;
    }

    [SerializeField] private float desiredAccuracyInMeters  = 1;
    [SerializeField] private float pollingIntervalInSeconds = 1;

    private   WaitForSecondsRealtime pollingInterval;
    private   bool                   ready;
    protected LocationData           Location = new LocationData();

    private LocationData lastLocation = new LocationData();

    /// <summary>
    /// Set true if the code is running on a device with GPS, the user has enabled GPS access and we have started the GPS tracking.
    /// </summary>
    
    public bool Running { get { return false; } }

    /// <summary>
    /// Set true if we do not have access to the GPS or its initialisation failed
    /// </summary>
    
    public bool Failed { get { return true; } }

    /// <summary>
    /// If all is good start the GPS tracking position. Remember that this eats battery.
    /// </summary>
    
    public void StartTracking() { }

    /// <summary>
    /// Coroutine that checks for changes to coordinates at set intervals. This will trigger an event for any who are listening.
    /// </summary>
    
    public IEnumerator StartPolling() {
      while (Initialising) {
        yield return pollingInterval;
      }

      while (!Failed) {
        UpdateLocation();

        if ((Math.Abs(Location.Latitude         - lastLocation.Latitude)         > 0.000005f) ||
            (Math.Abs(Location.Longitude        - lastLocation.Longitude)        > 0.000005f) ||
            (Math.Abs(Location.AltitudeInMeters - lastLocation.AltitudeInMeters) > 0.25f)) {
          lastLocation = Location;
        }

        yield return pollingInterval;
      }
    }

    protected void UpdateLocation() { }

    public virtual bool Initialising { get { return false; } }

    /// <summary>
    /// Start a coroutine to poll the GPS on the given MonoBehaviour.
    /// </summary>
    /// <param name="monoBehaviour">The MonoBehaviour that owns the polling coroutine</param>
    
    public void StartPolling(MonoBehaviour monoBehaviour) {
      monoBehaviour.StartCoroutine(StartPolling());
    }

    /// <summary>
    /// Turns off GPS tracking - saving battery.
    /// </summary>
    
    public void StopTracking() { }

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