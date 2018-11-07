using System;
using System.Collections;
using System.Collections.Generic;
using Askowl;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Decoupled.Mock {
  // ReSharper disable once InconsistentNaming
  /// <a href=""></a> //#TBD#// <inheritdoc />
  public class MockGPS : Mock<MockGPS.Service> {
    /// <a href="">Seed data for generating GPS points - to be set in the Unity Inspector</a> //#TBD#//
    [Serializable] public class StartingPoint {
      [SerializeField] internal float  Latitude               = -27.46850f;
      [SerializeField] internal float  Longitude              = 151.94379f;
      [SerializeField] internal float  RangeInMetres          = 100;
      [SerializeField] internal float  AltitudeInMeters       = 660;
      [SerializeField] internal float  AltitudeRange          = 5;
      [SerializeField] internal double Timestamp              = 0;
      [SerializeField] internal float  SecondsBetweenReadings = 5;
      [SerializeField] internal float  TimeAccelerationFactor = 1;
    }
    #pragma warning disable 0649
    [SerializeField] private StartingPoint startingPoint;
    #pragma warning restore 0649

    /// <inheritdoc />
    protected override void Awake() {
      base.Awake();
      MockService.locations = new Service.Locations() { StartingPoint = startingPoint };
      MockService.StartTracking();
    }

    /// <a href="">The actual mock service - that returns dummy data to the Unity application</a> //#TBD#// <inheritdoc />
    public class Service : GpsService {
      // ReSharper disable once InconsistentNaming
      internal Locations locations;
      private  bool      running, initialising, tracking;

      private float start;

      /// <inheritdoc />
      public override bool Offline => false;

      /// <inheritdoc />
      public override bool Initialising {
        get {
          SetState();
          return initialising;
        }
      }

      /// <inheritdoc />
      public override bool Running {
        get {
          SetState();
          return running;
        }
      }

      private void SetState() {
        if (running || !tracking) return;

        var elapsed = (int) (Time.realtimeSinceStartup - start);
        if (elapsed < (int) (1 * locations.StartingPoint.TimeAccelerationFactor)) return;

        initialising = false;
        running      = true;
      }

      /// <inheritdoc />
      public override void StartTracking() {
        start        = Time.realtimeSinceStartup;
        initialising = true;
        tracking     = true;
      }

      /// <inheritdoc />
      public override void StopTracking() { tracking = initialising = running = false; }

      /// <inheritdoc />
      protected override LocationData ReadLocation() {
        locations.MoveNext();
        return locations.Current;
      }

      /// <a href="">Retrieve semi-random locations restricted by StartingPoint</a> //#TBD#//
      public class Locations : IEnumerator<LocationData> {
        private StartingPoint startingPoint;

        internal StartingPoint StartingPoint {
          get => startingPoint;
          set {
            startingPoint = value;
            ResetToStart();
          }
        }

        private LocationData lastLocation;
        private float        range;
        private float        nextReadingTime, realSecondsBetweenReadings;

        /// <inheritdoc />
        public bool MoveNext() {
          if (Time.realtimeSinceStartup < nextReadingTime) return true;

          ResetToStart();
          lastLocation.Latitude  += Random.Range(min: -range, max: range);
          lastLocation.Longitude += Random.Range(min: -range, max: range);

          lastLocation.AltitudeInMeters += Random.Range(
            min: -StartingPoint.AltitudeRange,
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

        /// <inheritdoc />
        public LocationData Current { get; private set; }

        object IEnumerator.Current => Current;

        void IDisposable.Dispose() { }
      }
    }
  }
}