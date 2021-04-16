using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MazeMapper.Core;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MazeMapper.Web.Application.Controllers
{
    [ApiController]
    //  [controller] is the short key for the name of the controller: WeatherForecast without the ending "Controller".
    // this can be named to anything because the Route attribute will make sure this API is available in this address.
    // in order to connect to this API just go to the right route: //[web]/[controller]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
