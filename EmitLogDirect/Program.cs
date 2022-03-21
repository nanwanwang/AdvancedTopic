

using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory(){HostName = "localhost",UserName = "admin",Password = "admin"};
using var connection =factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare("direct_logs","direct");

var severity = args.Length > 0 ? args[0] : "info";
var message = args.Length > 1 ? string.Join(" ", args.Skip(1).ToArray()) : "hello world!";

var body = Encoding.UTF8.GetBytes(message);
channel.BasicPublish("direct_logs",severity,null,body);

Console.WriteLine($"[x] sent '{severity}':'{message}'");


Console.WriteLine("Press [Enter] to exit.");
Console.ReadLine();
