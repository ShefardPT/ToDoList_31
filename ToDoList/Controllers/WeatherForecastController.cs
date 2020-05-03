using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.Core;
using ToDoList.Data;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("weather_forecast")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ApplicationDbContext _ctx;

        public WeatherForecastController
            (ILogger<WeatherForecastController> logger,
            ApplicationDbContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();

            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();

            var dataToDb = result
                .Select(x => new Data.Entities.WeatherForecast()
                {
                    Date = x.Date,
                    Summary = x.Summary,
                    TemperatureC = x.TemperatureC,
                    TemperatureF = x.TemperatureF
                });

            _ctx.WeatherForecasts.AddRange(dataToDb);
            _ctx.SaveChanges();

            return result;
        }
    }
}
