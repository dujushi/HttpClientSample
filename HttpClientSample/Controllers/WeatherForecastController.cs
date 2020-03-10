using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HttpClientSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastClient _weatherForecastClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastClient weatherForecastClient)
        {
            _logger = logger;
            _weatherForecastClient = weatherForecastClient ?? throw new ArgumentNullException(nameof(weatherForecastClient));
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

        [HttpGet("AddWeatherForecastAsync")]
        public async Task AddWeatherForecastAsync()
        {
            var weatherForecast = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = 55,
                Summary = "Scorching"
            };
            await _weatherForecastClient.AddWeatherForecastAsync(weatherForecast, HttpContext.RequestAborted);
        }
    }
}
