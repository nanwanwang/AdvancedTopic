

using System.Text;
using System.Threading.Channels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory(){HostName = "localhost",UserName = "admin",Password = "admin"};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare("logs",ExchangeType.Fanout);

var queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(queueName,"logs","");

Console.WriteLine("[*] waiting for logs.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received+= (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"[x] {message}");
};

channel.BasicConsume(queueName,true,consumer);

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();