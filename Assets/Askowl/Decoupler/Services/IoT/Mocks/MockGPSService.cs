using System.Collections;
using System.Collections.Generic;
using Askowl;
using UnityEngine;

namespace Decoupled.Mock {
  public class MockGPSService : GPSService, IMock {
    private bool      running, offline, tracking, initialising;
    public  Locations locations = new Locations();

    private float start = Time.realtimeSinceStartup;

    public override bool Offline {
      get {
        SetState();
        return offline;
      }
    }

    public override bool Initialising {
      get {
        if (offline) return false;

        SetState();
        return initialising;
      }
    }

    public override bool Running {
      get {
        if (offline) return false;

        SetState();
        return running;
      }
    }

    private void SetState() {
      if (offline || running || !tracking) return;

      int elapsed = (int) (Time.realtimeSinceStartup - start);
      if (elapsed < (int) (5 * locations.TimeAccelerationFactor)) return;

      initialising = false;
      running      = true;
    }

    public override void StartTracking() {
      start        = Time.realtimeSinceStartup;
      initialising = true;
      tracking     = true;
    }

    public override void StopTracking() { tracking = initialising = running = false; }

    public override void UpdateLocation() {
      Location = locations.Current;
      locations.MoveNext();
    }

    public class Locations : IEnumerator<LocationData> {
      public float  StartLatitude          = -27.46850f;
      public float  StartLongitude         = 151.94379f;
      public float  RangeInMetres          = 100;
      public float  StartAltitudeInMeters  = 660;
      public float  AltitudeRange          = 5;
      public double Timestamp              = -1;
      public float  SecondsBetweenReadings = 5;
      public float  TimeAccelerationFactor = 1;

      private LocationData lastLocation;
      private float        range;
      private float        nextReadingTime, realSecondsBetweenReadings;

      public Locations() {
        if (Timestamp < 0) Timestamp = Clock.EpochTimeNow;
        range                      = RangeInMetres          * 0.0001f;
        realSecondsBetweenReadings = SecondsBetweenReadings / TimeAccelerationFactor;
        Reset();
      }

      public bool MoveNext() {
        if (Time.realtimeSinceStartup < nextReadingTime) return true;

        lastLocation.Latitude         += Random.Range(-range,         range);
        lastLocation.Longitude        += Random.Range(-range,         range);
        lastLocation.AltitudeInMeters += Random.Range(-AltitudeRange, AltitudeRange);
        lastLocation.Timestamp        += SecondsBetweenReadings;

        nextReadingTime += realSecondsBetweenReadings;
        Current         =  lastLocation;
        return true;
      }

      public void Reset() {
        nextReadingTime = Time.realtimeSinceStartup + realSecondsBetweenReadings;

        lastLocation.Latitude                   = StartLatitude;
        lastLocation.Longitude                  = StartLongitude;
        lastLocation.AltitudeInMeters           = StartAltitudeInMeters;
        lastLocation.Timestamp                  = Timestamp;
        lastLocation.VerticalAccuracyInMetres   = 10;
        lastLocation.HorizontalAccuracyInMetres = 65;
      }

      public LocationData Current { get; private set; }

      object IEnumerator.Current { get { return Current; } }

      public void Dispose() { }
    }
  }
}