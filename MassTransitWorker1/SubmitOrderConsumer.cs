using System.Threading.Tasks;
using MassTransit;
using MassTransitCommon;
using Microsoft.Extensions.Logging;

namespace MassTransitWorker1
{
    public class SubmitOrderConsumer:IConsumer<SubmitOrder>
    {
        private readonly ILogger<SubmitOrderConsumer> _logger;

        public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
           _logger.LogInformation($"Order Submitted:{context.Message.OrderId}");
          await context.Publish<OrderSubmitted>(new {context.Message.OrderId});
        }
    }
}