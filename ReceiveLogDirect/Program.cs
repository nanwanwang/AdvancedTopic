using System.Text;
using System.Threading.Channels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory(){HostName = "localhost",UserName = "admin",Password = "admin"};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare("direct_logs","direct");
var queueName= channel.QueueDeclare().QueueName;
if (args.Length < 1)
{
    Console.Error.WriteLine($"Usage: {Environment.GetCommandLineArgs()[0]} [info] [warning] [error]");
    Console.WriteLine("Press [Enter] to exit.");
    Console.ReadLine();
    Environment.ExitCode = 1;
    return;
}

foreach (var severity in args)
{
    channel.QueueBind(queueName,"direct_logs",severity);
}

Console.WriteLine("[*] waiting for message.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received+= (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var routingKey = ea.RoutingKey;
    Console.WriteLine($"[x] received:'{routingKey}':'{message}'");
};

channel.BasicConsume(queueName,true,consumer);

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();