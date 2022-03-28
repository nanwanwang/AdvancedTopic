using MassTransit;

namespace MqTest;

public class WeatherForecastConsumerDefinition:ConsumerDefinition<WeatherForecastConsumer>
{
    public WeatherForecastConsumerDefinition()
    {
        EndpointName = "weather-service";
        ConcurrentMessageLimit = 8;
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<WeatherForecastConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(100,200,500,800,1000));
        endpointConfigurator.UseInMemoryOutbox();
    }
}