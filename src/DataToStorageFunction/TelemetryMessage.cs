public class data
{
    public double? accelerometerX { get; set; }
    public double? accelerometerY { get; set; }
    public double? accelerometerZ { get; set; }
    public double? gyroscopeX { get; set; }
    public double? gyroscopeY { get; set; }
    public double? gyroscopeZ { get; set; }
    public double? humidity { get; set; }
    public double? magnetometerX { get; set; }
    public double? magnetometerY { get; set; }
    public double? magnetometerZ { get; set; }
    public double? pressure { get; set; }
    public double? temperature { get; set; }
    public Geolocation geolocation { get; set; }
    public Coordinates accelerometer { get; set; }
    public double? barometer { get; set; }
    public double? battery { get; set; }
    public Coordinates rotation { get; set; }
    public Coordinates magnetometer { get; set; }
}

public class Coordinates
{
    public double? x { get; set; }
    public double? y { get; set; }
    public double? z { get; set; }

}

public class TelemetryMessage
{
    public string deviceId { get; set; }
    public string deviceType { get; set; }
    public string enqueuedTime { get; set; }
    public string messageSource { get; set; }
    public data telemetry { get; set; }
}

public class Geolocation
{
    public double? lat { get; set; }
    public double? lon { get; set; }
    public double? alt { get; set; }
}

