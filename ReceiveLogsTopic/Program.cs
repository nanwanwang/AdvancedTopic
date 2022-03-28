using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory(){HostName = "localhost",UserName = "admin",Password = "admin"};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare("topic_logs","topic");
var queueName = channel.QueueDeclare().QueueName;
if (args.Length < 1)
{
    Console.Error.WriteLine($"Usage:{Environment.GetCommandLineArgs()[0]} [binding_key...]");
    Console.WriteLine("Press [enter] to exit.");
    Console.ReadLine();
    Environment.ExitCode = -1;
    return;
}

foreach (var bindingKey in args)
{
    channel.QueueBind(queueName,"topic_logs",bindingKey);
}
Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");

var consumer = new EventingBasicConsumer(channel);

consumer.Received+= (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var routingKey = ea.RoutingKey;
    Console.WriteLine(" [x] Received '{0}':'{1}'",
        routingKey,
        message);
};

channel.BasicConsume(queueName,true,consumer);
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
