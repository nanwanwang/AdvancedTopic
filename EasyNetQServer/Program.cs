

using EasyNetQ;
using EasyNetQ.Logging;
using EasyNetQModel;


var username = "admin";
var password = "admin";
var server = "localhost";
var vhost = "/";
LogProvider.SetCurrentLogProvider(ConsoleLogProvider.Instance);
using var bus = RabbitHutch.CreateBus($"amqp://{username}:{password}@{server}/{vhost}");
string? input;
Console.WriteLine("Enter a message: 'Quit' to quit.");
while ((input = Console.ReadLine()) != "Quit")
{
    await  bus.PubSub.PublishAsync(new TextMessage(){ Text = input},"X.Y");
    await bus.PubSub.PublishAsync(new TextMessage() {Text = input + "!!!!!!!"}, "A.B");
    Console.WriteLine("Message published!");
}



Console.ReadKey();