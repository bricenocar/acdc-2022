using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ACDC2022.Models
{
    public class Telemetry
    {
        [Key]       
        public string id { get; set; }
        public string? DeviceId { get; set; }
        public string? DeviceType { get; set; }
        public Data? Data { get; set; }
        public DateTime? Timestamp { get; set; }
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
        public Geolocation? Geolocation { get; set; }
        public double? Battery { get; set; }
        public double? Barometer { get; set; }
    }

    public class Geolocation
    {
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public double? Alt { get; set; }
    }
}
