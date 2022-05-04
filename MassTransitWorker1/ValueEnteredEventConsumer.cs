using System.Threading.Tasks;
using MassTransit;
using MassTransitCommon;
using Microsoft.Extensions.Logging;

namespace MassTransitWorker1
{
    public class ValueEnteredEventConsumer:IConsumer<ValueEntered>
    {
        private ILogger<ValueEnteredEventConsumer> _logger;

        public ValueEnteredEventConsumer(ILogger<ValueEnteredEventConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ValueEntered> context)
        {
           _logger.LogInformation($"Value:{context.Message.Value}");
           return  Task.CompletedTask;
        }
    }
}