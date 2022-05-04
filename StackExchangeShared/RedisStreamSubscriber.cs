using StackExchange.Redis;
using StackExchangeShared.Serialization;

namespace StackExchangeShared;

public class RedisStreamSubscriber:IStreamSubscriber
{
    private readonly ISerializer _serializer;
    private readonly ISubscriber _subscriber;

    public RedisStreamSubscriber(IConnectionMultiplexer connectionMultiplexer, ISerializer serializer)
    {
        _serializer = serializer;
        _subscriber = connectionMultiplexer.GetSubscriber();
    }

    public Task SubscribeAsync<T>(string topic, Action<T> handler) where T : class
        => _subscriber.SubscribeAsync(topic, (_, data) =>
        {
            var payload = _serializer.Deserialize<T>(data);
            if (payload is null)
                return;
            handler(payload);
        });
}