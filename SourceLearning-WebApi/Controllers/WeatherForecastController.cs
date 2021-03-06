using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

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
        private readonly IConfiguration _configuration;

        private readonly IOptionsSnapshot<DateTimeOptions> _optionsSnapshot;
        private readonly IOptions<DateTimeOptions> _options;
        private readonly IOptionsMonitor<DateTimeOptions> _optionsMonitor;
        private readonly IOptionsSnapshot<Book2Options> _book2Options;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,DependencyService4 service4, DependencyService3 service3, DependencyService2 service2, DependencyService1 service1, IEnumerable<IService> services, IConfiguration configuration, IOptionsSnapshot<DateTimeOptions> optionsSnapshot, IOptions<DateTimeOptions> options, IOptionsMonitor<DateTimeOptions> optionsMonitor, IOptionsSnapshot<Book2Options> book2Options)
        {
            _logger = logger;
            _service4 = service4;
            _service3 = service3;
            _service2 = service2;
            _service1 = service1;
            _services = services;
            _configuration = configuration;
            _optionsSnapshot = optionsSnapshot;
            _options = options;
            _optionsMonitor = optionsMonitor;
            _book2Options = book2Options;
        }


        
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

      

        [HttpGet]
        public IEnumerable<WeatherForecast> Get([FromServices] DependencyService4 dependencyService4,[FromServices] IOptions<BookOptions> _bookOptions)
        {

            var  book2Options=_book2Options.Value;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("----------------------------------");
           
            var options=  _options.Value;
            Console.WriteLine("options=>"+JsonSerializer.Serialize(options));

            var beijingOptions1 = _optionsSnapshot.Get(DateTimeOptions.Beijing);
            var toykoOptions1 = _optionsSnapshot.Get(DateTimeOptions.Tokyo);

            Console.WriteLine("beijingOptions1=> "+JsonSerializer.Serialize(beijingOptions1));
            Console.WriteLine("toykoOptions1=> "+JsonSerializer.Serialize(toykoOptions1));
            
            var beijingOptions2 = _optionsMonitor.Get(DateTimeOptions.Beijing);
            var toykoOptions2 = _optionsMonitor.Get(DateTimeOptions.Tokyo);
            Console.WriteLine("beijingOptions2=> "+JsonSerializer.Serialize(beijingOptions2));
            Console.WriteLine("toykoOptions2=> "+JsonSerializer.Serialize(toykoOptions2));
            Console.WriteLine("----------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(JsonSerializer.Serialize(_bookOptions.Value));
            var bookOptions = _configuration.GetSection(BookOptions.Book).Get<BookOptions>();
            // foreach (var item in  _configuration.AsEnumerable())
            // {
            //     Console.WriteLine(item.Key+"="+item.Value);
            // }

           
            Console.WriteLine(JsonSerializer.Serialize(bookOptions));
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
