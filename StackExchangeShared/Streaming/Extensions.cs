using Microsoft.Extensions.DependencyInjection;

namespace StackExchangeShared.Streaming;

public static class Extensions
{
    public static IServiceCollection AddRedisStreaming(this IServiceCollection services)
    {
        services.AddSingleton<IStreamPublisher, RedisStreamPublisher>()
            .AddSingleton<IStreamSubscriber, RedisStreamSubscriber>();
        return services;
    }
    
}