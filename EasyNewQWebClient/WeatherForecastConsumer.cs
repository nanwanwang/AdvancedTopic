// using EasyNetQ;
// using EasyNetQ.AutoSubscribe;
// using MqTestModel;
// using JsonSerializer = System.Text.Json.JsonSerializer;
//
// namespace EasyNewQWebClient;
//
// public class WeatherForecastConsumer:IConsume<WeatherForecast>
// {
//   
//     private IBus _bus;
//     private readonly ILogger<WeatherForecastConsumer> _logger;
//
//
//     public WeatherForecastConsumer(IBus bus,ILogger<WeatherForecastConsumer> logger)
//     {
//         _bus = bus;
//         _logger = logger;
//     }
//
//     [AutoSubscriberConsumer(SubscriptionId = "EasyNetQDemo.WeatherForecast")]
//     public void Consume(WeatherForecast message, CancellationToken cancellationToken = new CancellationToken())
//     {
//         _logger.LogInformation(JsonSerializer.Serialize(message));
//           
//         Console.WriteLine("Auto Subscribe:"+JsonSerializer.Serialize(message));
//         // var res =_bus.ToString();
//     }
// }