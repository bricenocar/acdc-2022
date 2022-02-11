using Microsoft.AspNetCore.Mvc;
using ACDC2022.Services;

namespace ACDC2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelemetryController : Controller
    {
        private readonly ITelemetryService _telemetryService;

        public TelemetryController(ITelemetryService telemetryService)
        {
            _telemetryService = telemetryService;
        }

        [HttpGet]
        [Route("GetTelemetry")]
        public async Task<IActionResult> GetTelemetry()
        {
            try
            {
                var telemetries = _telemetryService.GetTelemetriesWithGeolocation();
                return Ok(telemetries);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
