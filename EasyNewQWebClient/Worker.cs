using EasyNetQ;
using MqTestModel;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EasyNewQWebClient;

public class Worker:BackgroundService
{
    private readonly IBus _bus;
    private readonly ILogger<Worker> _logger;

    private int _runningState;
    private readonly SubscribeRequestsChannel _requestsChannel;
    private ISubscriptionResult _subscriptionResult;


    public Worker(IBus bus, ILogger<Worker> logger, SubscribeRequestsChannel requestsChannel)
    {
        _bus = bus;
        _logger = logger;
        _requestsChannel = requestsChannel;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var request in _requestsChannel.Requests.Reader.ReadAllAsync(stoppingToken))
        {
            switch (request)
            {
                case StartSubscribe startSubscribe:
                     _subscriptionResult = await _bus.PubSub.SubscribeAsync<WeatherForecast>(
                        "EasyNetQDemo.WeatherForecast",
                        forecast => { _logger.LogInformation("log rizhi: " + JsonSerializer.Serialize(forecast)); });
                    break;
                case StopSubscribe:
                    _subscriptionResult?.Dispose();
                    break;
                default:
                    _ = Task.CompletedTask;
                    break;
            }
        }
        

     
    }
}