using ACDC2022.Data;
using ACDC2022.Models;
using Microsoft.EntityFrameworkCore;

namespace ACDC2022.Repositories
{
    public interface ITelemetryRepository
    {
        List<Telemetry> GetTelemetriesWithGeolocation();
        void DeleteAllTelemetries();
    }

    public class TelemetryRepository : ITelemetryRepository
    {
        private readonly ACDC2022DbContext _dbContext;

        public TelemetryRepository(ACDC2022DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Telemetry> GetTelemetriesWithGeolocation()
        {
            var dbTelemetries = _dbContext.Telemetries.Where(x => x.DeviceType == "IoT Plug and Play mobile").OrderByDescending(x => x.Timestamp).ToList();
            return dbTelemetries.Where(x => x.Data.Geolocation != null).GroupBy(x => x.DeviceId).Select(x => x.First()).ToList();
        }

        public void DeleteAllTelemetries()
        {
            var telemetries = _dbContext.Telemetries.ToList();
            _dbContext.Telemetries.RemoveRange(telemetries);
            _dbContext.SaveChanges();
        }
    }
}
