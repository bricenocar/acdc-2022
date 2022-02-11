using ACDC2022.Models;
using ACDC2022.Repositories;

namespace ACDC2022.Services
{
    public interface ITelemetryService
    {
        List<Telemetry> GetTelemetriesWithGeolocation();
    }

    public class TelemetryService : ITelemetryService
    {
        private readonly ITelemetryRepository _telemetryRepository;

        public TelemetryService(ITelemetryRepository telemetryRepository)
        {
            _telemetryRepository = telemetryRepository;
        }

        public List<Telemetry> GetTelemetriesWithGeolocation()
        {
            // Get user
            return _telemetryRepository.GetTelemetriesWithGeolocation();
        }
    }
}
