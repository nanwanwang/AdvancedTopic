

using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory(){HostName = "localhost",UserName = "admin",Password = "admin"};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare("queue", durable: true, false, false, null);
channel.BasicQos(0,1,false);
Console.WriteLine("[*] Waiting for messages");
var consumer = new EventingBasicConsumer(channel);

consumer.Received+= (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
    int dots = message.Split(".").Length - 1;
    Thread.Sleep(dots * 1000);
    Console.WriteLine("[x] Done");
    channel?.BasicAck(ea.DeliveryTag,false);
};
channel.BasicConsume("queue",true,consumer);
Console.WriteLine(" Press [enter] to exit.");

Console.ReadLine();

