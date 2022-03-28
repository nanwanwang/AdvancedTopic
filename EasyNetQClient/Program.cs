

using System.Net.Mime;
using EasyNetQ;
using EasyNetQ.Logging;
using EasyNetQModel;
using MqTestModel;

var username = "admin";
var password = "admin";
var server = "localhost";
var vhost = "/";
LogProvider.SetCurrentLogProvider(ConsoleLogProvider.Instance);
using var bus = RabbitHutch.CreateBus($"amqp://{username}:{password}@{server}/{vhost}");
await bus.PubSub.SubscribeAsync<WeatherForecast>("test", HandleTextMessage);

Console.WriteLine("Listening for messages. Hit <return> to quit.");
Console.ReadLine();


static void HandleTextMessage(WeatherForecast weatherForecast)
{
    Console.ForegroundColor= ConsoleColor.Red;
    Console.WriteLine( System.Text.Json.JsonSerializer.Serialize(weatherForecast));
    Console.ResetColor();
}