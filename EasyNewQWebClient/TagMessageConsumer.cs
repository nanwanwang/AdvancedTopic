using System.Text.Json;
using EasyNetQ.AutoSubscribe;
using IOServer.Entity.Shared.Models;
using MqTestModel;

namespace EasyNewQWebClient;

public class PublishTagMessageConsumer:IConsumeAsync<DeviceMessage>
{
    private readonly ILogger<PublishTagMessageConsumer> _logger;

    public PublishTagMessageConsumer(ILogger<PublishTagMessageConsumer> logger)
    {
        _logger = logger;
    }

    [AutoSubscriberConsumer(SubscriptionId = "EasyNetQDemo.PublishTagMessage")]
    [ForTopic("topic.test01")]
    [ForTopic("topic.test02")]
    [ForTopic("topic.test03")]
    [ForTopic("topic.test04")]
    [ForTopic("topic.test05")]
    [ForTopic("topic.test06")]
    public Task ConsumeAsync(DeviceMessage message, CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogInformation(JsonSerializer.Serialize(message));
        return  Task.CompletedTask;
    }
}