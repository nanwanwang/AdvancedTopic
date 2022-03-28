//using MassTransit;

using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using MqTestModel;

namespace MqTestPublish.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    readonly IBus _bus;

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,  IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    [HttpPost("publish")]
    public async  Task<ActionResult> Publish()
    {
        //Masstransit
        // await _bus.Publish<WeatherForecast>(new WeatherForecast()
        // {
        //     Date = DateTime.Now.AddDays(1),
        //     TemperatureC = Random.Shared.Next(-20, 55),
        //     Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        // });
        
        //easynetq
        await _bus.PubSub.PublishAsync(new WeatherForecast()
        {
            Date = DateTime.Now.AddDays(1),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });
        return Ok();
    }


    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}