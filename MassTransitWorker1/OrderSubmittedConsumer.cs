using System.Threading.Tasks;
using MassTransit;
using MassTransitCommon;
using Microsoft.Extensions.Logging;

namespace MassTransitWorker1
{
    public class OrderSubmittedConsumer:IConsumer<OrderSubmitted>
    {
        private readonly ILogger<OrderSubmittedConsumer> _logger;

        public OrderSubmittedConsumer(ILogger<OrderSubmittedConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<OrderSubmitted> context)
        {
            _logger.LogInformation($"Order Submitted:{context.Message.OrderId}");
            await context.Publish<OrderSubmitted>(new {context.Message.OrderId});
        }
    }
}