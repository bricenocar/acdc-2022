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

        //[HttpGet]
        //[Route("GetTelemetriesWithGeolocation")]
        //public async Task<IActionResult> GetTelemetriesWithGeolocation()
        //{
        //    try
        //    {
        //        var telemetries = _telemetryService.GetTelemetriesWithGeolocation();
        //        return Ok(telemetries);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex);
        //    }
        //}

        //[HttpDelete]
        //[Route("DeleteAllTelemetries")]
        //public async Task<IActionResult> DeleteAllTelemetries()
        //{
        //    try
        //    {
        //        _telemetryService.DeleteAllTelemetries();
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex);
        //    }
        //}
    }
}
