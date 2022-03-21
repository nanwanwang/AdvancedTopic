// See https://aka.ms/new-console-template for more information

using System.Text;
using RabbitMQ.Client;




    var factory = new ConnectionFactory(){HostName = "192.168.1.103",UserName = "admin", Password = "admin"};


    using var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();

    channel.QueueDeclare("hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

    string message = "hello world";
    var body = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish("", routingKey: "hello", basicProperties: null, body);
    Console.WriteLine($"[x] sent {message} ");

    Console.WriteLine("Press [enter] to exit.");
    Console.ReadLine();




