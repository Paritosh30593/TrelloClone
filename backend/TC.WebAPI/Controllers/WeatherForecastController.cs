using Microsoft.AspNetCore.Mvc;
using TC.WebAPI.Controllers.Base;

namespace TC.WebAPI.Controllers
{
    public class WeatherForecastController : BaseController
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get a list of weather forecasts with random data.
        /// </summary>
        /// <returns>A list of weather forecasts.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAll()
        {
            return Ok(Summaries.Select(s => s));
        }
    }
}
