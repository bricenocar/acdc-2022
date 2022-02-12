using ACDC2022.Services;
using ACDC2022.Models;
using Microsoft.AspNetCore.SignalR;

namespace ACDC2022.Hubs
{
    public class TelemetryHub : Hub
    {
        private readonly ITelemetryService _telemetryService;

        public TelemetryHub(ITelemetryService telemetryService)
        {
            _telemetryService = telemetryService;
        }

        public Task RequestData(string param)
        {
            var telemetries = new List<Telemetry>();

            if (string.IsNullOrEmpty(param))
            {
                telemetries = _telemetryService.GetTelemetriesWithGeolocation();
            }
            else
            {
                var deviceId = param.Split(',')[0];
                var longitud = param.Split(',')[1];
                var latitude = param.Split(',')[2];

                var telemetry = new Telemetry()
                {
                    DeviceId = deviceId,
                    id = Guid.NewGuid().ToString(),
                    Data = new Models.Data()
                    {
                        Geolocation = new Geolocation()
                        {
                            //Lat = double.Parse(latitude.Replace('.', ',')),
                            //Lon = double.Parse(longitud.Replace('.', ','))
                            Lat = double.Parse(latitude),
                            Lon = double.Parse(longitud)
                        }
                    }
                };

                telemetries.Add(telemetry);
            }

            return Clients.All.SendAsync("responseData", telemetries);
        }
    }
}
