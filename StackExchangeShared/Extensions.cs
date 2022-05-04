using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace StackExchangeShared;

public static class Extensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("redis");
        var options = new RedisOptions();
        section.Bind(options);
        services.Configure<RedisOptions>(section);
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(options.ConnectionString));
        return services;
    }
    
}