// using EasyNetQ.AutoSubscribe;
// using MqTestModel;
//
// namespace EasyNewQWebClient;
//
// public class StringConsumer:IConsume<string>
// {
//     private readonly ILogger<StringConsumer> _logger;
//
//     public StringConsumer(ILogger<StringConsumer> logger)
//     {
//         _logger = logger;
//     }
//
//     [AutoSubscriberConsumer(SubscriptionId = "EasyNetQDemo.string")]
//     public void Consume(string message, CancellationToken cancellationToken = new CancellationToken())
//     {
//         _logger.LogInformation(message);
//           
//         // Console.WriteLine("Auto Subscribe:"+JsonSerializer.Serialize(message));
//         // var res =_bus.ToString();
//     }
// }