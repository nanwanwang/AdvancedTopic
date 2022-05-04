using EasyNetQ.AutoSubscribe;
using IOServer.Entity.Shared.Models;
using Newtonsoft.Json;

namespace EasyNewQWebClient;

public class NormalChargingConsumer:IConsumeAsync<List<string>>
{
    private readonly ILogger<NormalChargingConsumer> _logger;

    public NormalChargingConsumer(ILogger<NormalChargingConsumer> logger)
    {
        _logger = logger;
    }
    
    [AutoSubscriberConsumer(SubscriptionId = "EasyNetQDemo.NormalCharging")]
    [ForTopic("topic.R001007.one")]
    [ForTopic("topic.R001007.two")]
    [ForTopic("topic.R001008.one")]
    [ForTopic("topic.R001008.two")]
    public Task ConsumeAsync(List<string> message, CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogInformation($"NormalChargingConsumer:{string.Join(",", message)}");
        return  Task.CompletedTask;
    }
}