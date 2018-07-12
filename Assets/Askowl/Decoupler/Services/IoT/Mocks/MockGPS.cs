using System;
using System.Collections;
using System.Collections.Generic;
using Askowl;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Decoupled.Mock {
  public class MockGPS : Mock<MockGPS.Service> {
    protected override void Awake() {
      base.Awake();
      MockService.locations = new Service.Locations();
      MockService.StartTracking();
    }

    public class Service : GPSService {
      private bool      running, initialising, tracking;
      public  Locations locations;

      private float start;

      public override bool Offline => false;

      public override bool Initialising {
        get {
          SetState();
          return initialising;
        }
      }

      public override bool Running {
        get {
          SetState();
          return running;
        }
      }

      private void SetState() {
        if (running || !tracking) return;

        int elapsed = (int) (Time.realtimeSinceStartup - start);
        if (elapsed < (int) (1 * locations.TimeAccelerationFactor)) return;

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
        locations.MoveNext();
        Location = locations.Current;
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
          ResetToStart();
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

        void IEnumerator.Reset() { ResetToStart(); }

        private void ResetToStart() {
          nextReadingTime = Time.realtimeSinceStartup + realSecondsBetweenReadings;

          lastLocation.Latitude                   = StartLatitude;
          lastLocation.Longitude                  = StartLongitude;
          lastLocation.AltitudeInMeters           = StartAltitudeInMeters;
          lastLocation.Timestamp                  = Timestamp;
          lastLocation.VerticalAccuracyInMetres   = 10;
          lastLocation.HorizontalAccuracyInMetres = 65;
        }

        public LocationData Current { get; private set; }

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }
      }
    }
  }
}