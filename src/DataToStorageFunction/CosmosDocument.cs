using System;
using Microsoft.Azure.Functions.Worker;

public class CosmosDocument
{
    [CosmosDBOutput("ACDC2022", "Telemetries",
        ConnectionStringSetting = "CosmosDbConnectionString", CreateIfNotExists = true)]
    public TelemetryDocument Document { get; set; }
}

public class TelemetryDocument
{
    public string DeviceId { get; set; }
    public string DeviceType { get; set; }
    public Data Data { get; set; }
    public string Timestamp { get; set; }
}

public class Data
{
    public double? AccelerometerX { get; set; }
    public double? AccelerometerY { get; set; }
    public double? AccelerometerZ { get; set; }

    public double? GyroscopeX { get; set; }
    public double? GyroscopeY { get; set; }
    public double? GyroscopeZ { get; set; }

    public double? Humidity { get; set; }
    public double? MagnetometerX { get; set; }
    public double? MagnetometerY { get; set; }
    public double? MagnetometerZ { get; set; }

    public double? Pressure { get; set; }
    public double? Temperature { get; set; }
    public GeolocationOut Geolocation { get; set; }
    public double? Battery { get; set; }
    public double? Barometer { get; set; }
}

public class GeolocationOut
{
    public double? Lat { get; set; }
    public double? Lon { get; set; }
    public double? Alt { get; set; }
}