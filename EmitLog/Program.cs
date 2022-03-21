
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory() {HostName = "localhost", UserName = "admin", Password = "admin"};
using var connection = factory.CreateConnection();
var channel = connection.CreateModel();
channel.ExchangeDeclare("logs",ExchangeType.Fanout);
var message = GetMessage(args);
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish("logs","",null,body);
Console.WriteLine($"[x] sent {message}");
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

string GetMessage(string[] args)
{
    return args.Length > 0 ? string.Join(" ", args) : "info: Hello world!";
}