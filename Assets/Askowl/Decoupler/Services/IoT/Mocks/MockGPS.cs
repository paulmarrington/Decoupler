using System;
using System.Collections;
using System.Collections.Generic;
using Askowl;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Decoupled.Mock {
  public class MockGPS : Mock<MockGPS.Service> {
    [Serializable]
    public class StartingPoint {
      // ReSharper disable Unity.RedundantSerializeFieldAttribute

      [SerializeField] internal float  Latitude               = -27.46850f;
      [SerializeField] internal float  Longitude              = 151.94379f;
      [SerializeField] internal float  RangeInMetres          = 100;
      [SerializeField] internal float  AltitudeInMeters       = 660;
      [SerializeField] internal float  AltitudeRange          = 5;
      [SerializeField] internal double Timestamp              = 0;
      [SerializeField] internal float  SecondsBetweenReadings = 5;
      [SerializeField] internal float  TimeAccelerationFactor = 1;

      // ReSharper restore Unity.RedundantSerializeFieldAttribute
    }

    [SerializeField] internal StartingPoint startingPoint;

    protected override void Awake() {
      base.Awake();
      MockService.locations = new Service.Locations() {StartingPoint = startingPoint};
      MockService.StartTracking();
    }

    public class Service : GPSService {
      internal Locations locations;
      private  bool      running, initialising, tracking;

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
        if (elapsed < (int) (1 * locations.StartingPoint.TimeAccelerationFactor)) return;

        initialising = false;
        running      = true;
      }

      public override void StartTracking() {
        start        = Time.realtimeSinceStartup;
        initialising = true;
        tracking     = true;
      }

      public override void StopTracking() { tracking = initialising = running = false; }

      protected override LocationData ReadLocation() {
        locations.MoveNext();
        return locations.Current;
      }

      public class Locations : IEnumerator<LocationData> {
        private StartingPoint startingPoint;

        internal StartingPoint StartingPoint {
          get { return startingPoint; }
          set {
            startingPoint = value;
            ResetToStart();
          }
        }

        private LocationData lastLocation;
        private float        range;
        private float        nextReadingTime, realSecondsBetweenReadings;

        public bool MoveNext() {
          if (Time.realtimeSinceStartup < nextReadingTime) return true;

          ResetToStart();
          lastLocation.Latitude  += Random.Range(min: -range, max: range);
          lastLocation.Longitude += Random.Range(min: -range, max: range);

          lastLocation.AltitudeInMeters += Random.Range(min: -StartingPoint.AltitudeRange,
                                                        max: StartingPoint.AltitudeRange);

          lastLocation.Timestamp += StartingPoint.SecondsBetweenReadings;

          nextReadingTime += realSecondsBetweenReadings;
          Current         =  lastLocation;
          return true;
        }

        void IEnumerator.Reset() { ResetToStart(); }

        private void ResetToStart() {
          if (StartingPoint.Timestamp <= 0) StartingPoint.Timestamp = Clock.EpochTimeNow;
          range = StartingPoint.RangeInMetres * 0.0001f;

          realSecondsBetweenReadings = StartingPoint.SecondsBetweenReadings /
                                       StartingPoint.TimeAccelerationFactor;

          nextReadingTime = Time.realtimeSinceStartup + realSecondsBetweenReadings;

          lastLocation.Latitude                   = StartingPoint.Latitude;
          lastLocation.Longitude                  = StartingPoint.Longitude;
          lastLocation.AltitudeInMeters           = StartingPoint.AltitudeInMeters;
          lastLocation.Timestamp                  = Clock.EpochTimeNow;
          lastLocation.VerticalAccuracyInMetres   = 2  + Random.Range(min: 1, max: 10);
          lastLocation.HorizontalAccuracyInMetres = 10 + Random.Range(min: 4, max: 55);
        }

        public LocationData Current { get; private set; }

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }
      }
    }
  }
}