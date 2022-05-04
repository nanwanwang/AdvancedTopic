using StackExchangeShared;

namespace StackExchangeClientSample;

public class Worker:BackgroundService
{
    private readonly IStreamSubscriber _subscriber;
    private readonly ILogger<Worker> _logger;

    public Worker(IStreamSubscriber subscriber, ILogger<Worker> logger)
    {
        _subscriber = subscriber;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _subscriber.SubscribeAsync<string>("12", data =>
        {
            _logger.LogInformation(data);
        });
    }
}