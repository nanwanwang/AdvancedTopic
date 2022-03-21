using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory() {HostName = "localhost", UserName = "admin", Password = "admin"};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare("hello", durable: false, false, false, null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"[x] received {message}");
};

channel.BasicConsume("hello", true, consumer);
Console.WriteLine("Press [Enter] to exit ");
Console.ReadLine();