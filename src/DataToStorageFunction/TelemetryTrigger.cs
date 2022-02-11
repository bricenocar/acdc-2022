using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Collections.Generic;

namespace TelemetryEvents
{
    public class TelemetryTrigger
    {
        private readonly ILogger _logger;

        public TelemetryTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TelemetryTrigger>();
        }

        [Function("TelemetryTrigger")]
        public CosmosDocument Run([EventHubTrigger("evh-acdc2022", Connection = "evhacdc2022_RootManageSharedAccessKey_EVENTHUB")] string[] input)
        {
            _logger.LogInformation($"First Event Hubs triggered message: {input[0]}");
            var documents = new List<CosmosDocument>();
            foreach (string item in input)
            {

                var telemetryMessage = JsonSerializer.Deserialize<TelemetryMessage>(item);
                var data = new Data
                {
                    Humidity = telemetryMessage.telemetry.humidity,
                    Pressure = telemetryMessage.telemetry.pressure,
                    Temperature = telemetryMessage.telemetry.temperature,
                    Battery = telemetryMessage.telemetry.battery
                };
                if (telemetryMessage.telemetry.accelerometer != null && telemetryMessage.telemetry.accelerometer.x != null)
                {
                    // var accelerometer = JsonSerializer.Deserialize<Coordinates>(telemetryMessage.telemetry.accelerometer);
                    data.AccelerometerX = telemetryMessage.telemetry.accelerometer.x;
                    data.AccelerometerY = telemetryMessage.telemetry.accelerometer.y;
                    data.AccelerometerZ = telemetryMessage.telemetry.accelerometer.z;
                }
                else
                {
                    data.AccelerometerX = telemetryMessage.telemetry.accelerometerX;
                    data.AccelerometerY = telemetryMessage.telemetry.accelerometerY;
                    data.AccelerometerZ = telemetryMessage.telemetry.accelerometerZ;


                }
                if (telemetryMessage.telemetry.magnetometer != null && telemetryMessage.telemetry.magnetometer.x != null)
                {
                    // var magnetometer = JsonSerializer.Deserialize<Coordinates>(telemetryMessage.telemetry.magnetometer);
                    data.MagnetometerX = telemetryMessage.telemetry.magnetometer.x;
                    data.MagnetometerY = telemetryMessage.telemetry.magnetometer.y;
                    data.MagnetometerZ = telemetryMessage.telemetry.magnetometer.z;
                }
                else
                {
                    data.MagnetometerX = telemetryMessage.telemetry.magnetometerX;
                    data.MagnetometerY = telemetryMessage.telemetry.magnetometerY;
                    data.MagnetometerZ = telemetryMessage.telemetry.magnetometerZ;

                }
                if (telemetryMessage.telemetry.geolocation != null && telemetryMessage.telemetry.geolocation.lat != null)
                {
                    data.Geolocation = new GeolocationOut
                    {
                        Lat = telemetryMessage.telemetry.geolocation.lat,
                        Lon = telemetryMessage.telemetry.geolocation.lon,
                        Alt = telemetryMessage.telemetry.geolocation.alt
                    };
                }
                var document = new CosmosDocument
                {
                    Document = new TelemetryDocument
                    {
                        DeviceId = telemetryMessage.deviceId,
                        DeviceType = telemetryMessage.deviceType,
                        Timestamp = telemetryMessage.enqueuedTime,
                        Data = data,
                    }
                };
                documents.Add(document);
            }
            return documents[0];
        }
    }


}
