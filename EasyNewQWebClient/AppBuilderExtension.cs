using System.Reflection;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;

namespace EasyNewQWebClient;

public static class AppBuilderExtension
{
    public static IApplicationBuilder UseSubscribe(this IApplicationBuilder appBulider, string subscriptionIdPrefix,
        Assembly[] assemblies)
    {
        var services = appBulider.ApplicationServices.CreateScope().ServiceProvider;
        var lifeTime = services.GetService<IHostApplicationLifetime>();
        var bus = services.GetService<IBus>();
        lifeTime!.ApplicationStarted.Register(() =>
        {
            var subscriber = new AutoSubscriber(bus, subscriptionIdPrefix);
            subscriber.Subscribe(assemblies);
            subscriber.SubscribeAsync(assemblies);
        });
        lifeTime.ApplicationStopped.Register(() => bus!.Dispose());
        return appBulider;
    }
}