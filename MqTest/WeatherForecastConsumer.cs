using MassTransit;
using MqTestModel;

namespace MqTest;

public class WeatherForecastConsumer:IConsumer<WeatherForecast>
{
    private readonly ILogger<WeatherForecastConsumer> _logger;

    public WeatherForecastConsumer(ILogger<WeatherForecastConsumer> logger)
    {
        _logger = logger;
    }

    public  Task Consume(ConsumeContext<WeatherForecast> context)
    {
        _logger.LogInformation($"WeatherForecast Summary: {context.Message.Summary},{context.Message.Date}" );
       // context.Publish<we
       return Task.CompletedTask;
    }
}