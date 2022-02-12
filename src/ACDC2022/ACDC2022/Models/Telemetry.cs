namespace ACDC2022.Models;

public class Telemetry
{
    public string DeviceId { get; set; }
    public Data Data { get; set; }
    public string Timestamp { get; set; }
    public string DeviceType { get; set; }
}

public class Data
{
    public string AccelerometerX { get; set; }
    public string AccelerometerY { get; set; }
    public string AccelerometerZ { get; set; }
    public string GyroscopeX { get; set; }
    public string GyroscopeY { get; set; }
    public string GyroscopeZ { get; set; }
    public string Humidity { get; set; }
    public string MagnetometerX { get; set; }
    public string MagnetometerY { get; set; }
    public string MagnetometerZ { get; set; }
    public string Pressure { get; set; }
    public string Temperature { get; set; }
}
