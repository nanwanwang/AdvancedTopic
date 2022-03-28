using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory(){HostName = "localhost",UserName = "admin",Password = "admin"};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare("topic_logs","topic");
var routingKey = args.Length>0?args[0]:"anonymous.info";
var message = args.Length >1 ?string.Join(" ",args.Skip(1).ToArray()):"Hello world!";
var body = Encoding.UTF8.GetBytes(message);
channel.BasicPublish("topic_logs",routingKey,null,body);

Console.WriteLine($"[x] sent '{routingKey}':'{message}'");