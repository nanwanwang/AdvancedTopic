using EasyNetQ;
using EasyNetQ.Logging;
using EasyNetQModel;

var username = "admin";
var password = "admin";
var server = "localhost";
var vhost = "/";
LogProvider.SetCurrentLogProvider(ConsoleLogProvider.Instance);
using var bus = RabbitHutch.CreateBus($"amqp://{username}:{password}@{server}/{vhost}");
await bus.PubSub.SubscribeAsync<TextMessage>("test2", HandleTextMessage,x=>x.WithTopic("*.B"));

Console.WriteLine("client2 Listening for messages. Hit <return> to quit.");
Console.ReadLine();


static void HandleTextMessage(TextMessage textMessage)
{
    Console.ForegroundColor= ConsoleColor.Red;
    Console.WriteLine($"Got message:{textMessage.Text}");
    Console.ResetColor();
}