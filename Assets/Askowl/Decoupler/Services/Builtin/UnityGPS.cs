using UnityEngine;

namespace Askowl.Mars {
  /// <inheritdoc />
  /// <summary>
  /// Provide access to updates in position from the phone/device GPS
  /// </summary>
  [CreateAssetMenu(menuName = "Custom Assets/GPS")]
  public class UnityGps : Decoupled.GPS {
    private readonly LocationService deviceLocaton = Input.location;

    /// <inheritdoc />
    protected override void OnEnable() {
      base.OnEnable();
      PollingInterval = new WaitForSecondsRealtime(PollingIntervalInSeconds);
      StartTracking();
    }

    /// <inheritdoc />
    protected override void OnDisable() {
      StopTracking();
      base.OnDisable();
    }

    /// <inheritdoc />
    public override bool Running {
      get { return (deviceLocaton.status == LocationServiceStatus.Running); }
    }

    /// <inheritdoc />
    public override bool Failed {
      get {
        return (!deviceLocaton.isEnabledByUser ||
                (deviceLocaton.status == LocationServiceStatus.Failed));
      }
    }

    /// <inheritdoc />
    public sealed override void StartTracking() {
      if (deviceLocaton.isEnabledByUser && !Running) {
        deviceLocaton.Start(DesiredAccuracyInMeters, UpdateDistanceInMeters);
      }
    }

    /// <inheritdoc />
    protected override void UpdateLocation() {
      Location.Latitude                   = deviceLocaton.lastData.latitude;
      Location.Longitude                  = deviceLocaton.lastData.longitude;
      Location.AltitudeInMeters           = deviceLocaton.lastData.altitude;
      Location.Timestamp                  = deviceLocaton.lastData.timestamp;
      Location.VerticalAccuracyInMetres   = deviceLocaton.lastData.verticalAccuracy;
      Location.HorizontalAccuracyInMetres = deviceLocaton.lastData.horizontalAccuracy;
    }

    /// <inheritdoc />
    public override bool Initialising {
      get { return deviceLocaton.status == LocationServiceStatus.Initializing; }
    }

    /// <inheritdoc />
    public override void StopTracking() {
      if (deviceLocaton.isEnabledByUser && Running) deviceLocaton.Stop();
    }
  }
}