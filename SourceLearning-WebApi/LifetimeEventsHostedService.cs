using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SourceLearning_WebApi
{
    public class LifetimeEventsHostedService:IHostedService
    {
        private readonly ILogger<LifetimeEventsHostedService> _logger;

        private readonly IHostApplicationLifetime _appLifeTime;
        public LifetimeEventsHostedService(ILogger<LifetimeEventsHostedService> logger, IHostApplicationLifetime appLifeTime)
        {
            _logger = logger;
            _appLifeTime = appLifeTime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifeTime.ApplicationStarted.Register(OnStarted);
            _appLifeTime.ApplicationStopped.Register(OnStopped);
            _appLifeTime.ApplicationStopping.Register(OnStopping);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return  Task.CompletedTask;
        }

        private void OnStarted()
        {
            _logger.LogInformation("App Started");
        }

        private void OnStopping()
        {
            _logger.LogInformation("App Stopping");
        }

        private void OnStopped()
        {
            _logger.LogInformation("App Stopped");
        }
        
    }
}