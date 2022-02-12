using ACDC2022.Data;
using ACDC2022.Models;

namespace ACDC2022.Repositories;

public interface ITelemetryRepository
{
    List<Telemetry> GetTelemetriesWithGeolocation();
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
        return _dbContext.Telemetries.Where(x => x.DeviceType == "IoT Plug and Play mobile").ToList();
    }
}
