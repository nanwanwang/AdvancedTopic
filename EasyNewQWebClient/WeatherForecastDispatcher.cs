using EasyNetQ.AutoSubscribe;

namespace EasyNewQWebClient;

public class WeatherForecastDispatcher:IAutoSubscriberMessageDispatcher
{

    private readonly IServiceProvider _serviceProvider;


    public WeatherForecastDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
       
    }

    public void Dispatch<TMessage, TConsumer>(TMessage message, CancellationToken cancellationToken = new CancellationToken()) where TMessage : class where TConsumer : class, IConsume<TMessage>
    {
        var consumer = _serviceProvider.GetRequiredService<TConsumer>();
        try
        {
            consumer.Consume(message);
        }
        finally
        {
            //_serviceProvider.Release(consumer);
        }
    }

    public async Task DispatchAsync<TMessage, TConsumer>(TMessage message,
        CancellationToken cancellationToken = new CancellationToken()) where TMessage : class where TConsumer : class, IConsumeAsync<TMessage>
    {
        try
        {
            TConsumer consumer = _serviceProvider.GetRequiredService<TConsumer>();
            await consumer.ConsumeAsync(message, cancellationToken);
        }
        catch (Exception e)
        {
            //_logger.LogError(e, $"创建消费者或者消费异常");
            throw;
        }
    }
}