

using System.Text;
using RabbitMQ.Client;

var  factory = new ConnectionFactory(){ HostName = "localhost",UserName = "admin",Password = "admin"};

using var  connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare("queue", true, false, false, null);
var message = GetMessage(args);

var body = Encoding.UTF8.GetBytes(message);
var properties = channel.CreateBasicProperties();
properties.Persistent = true;


channel.BasicPublish("","queue",properties,body);


string GetMessage(string[] args)
{
    return args.Length > 0 ? string.Join(" ", args) : "hello world!";
}