using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SourceLearning_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        public  IUserService UserService { get; set; }
        private readonly IEnumerable<IService> _services;
        private readonly DependencyService1 _service1;
        private readonly DependencyService2 _service2;
        private readonly DependencyService3 _service3;
        private readonly DependencyService4 _service4;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,DependencyService4 service4, DependencyService3 service3, DependencyService2 service2, DependencyService1 service1, IEnumerable<IService> services)
        {
            _logger = logger;
            _service4 = service4;
            _service3 = service3;
            _service2 = service2;
            _service1 = service1;
            _services = services;
        }


        
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

      

        [HttpGet]
        public IEnumerable<WeatherForecast> Get([FromServices] DependencyService4 dependencyService4)
        {
            Console.WriteLine(UserService.Get());

            if (dependencyService4 != null)
            {
                Console.WriteLine("from services  func ");
            }
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
