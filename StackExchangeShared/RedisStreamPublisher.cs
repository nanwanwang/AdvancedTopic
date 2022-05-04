using StackExchange.Redis;
using StackExchangeShared.Serialization;

namespace StackExchangeShared;

public class RedisStreamPublisher:IStreamPublisher
{
    private readonly ISerializer _serializer;
    private readonly ISubscriber _subscriber;

    public RedisStreamPublisher(IConnectionMultiplexer connectionMultiplexer, ISerializer serializer)
    {
        _serializer = serializer;
        _subscriber = connectionMultiplexer.GetSubscriber();
    }

    public Task PublishAsync<T>(string topic, T data) where T : class
    {
        var playload = _serializer.Serialize(data);
        return _subscriber.PublishAsync(topic, playload);
    }
}