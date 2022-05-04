using MassTransit;

namespace MassTransitWorker1
{
    public class SubmitOrderConsumerDefinition:ConsumerDefinition<SubmitOrderConsumer>
    {
        public SubmitOrderConsumerDefinition()
        {
            EndpointName = "order-service";
            ConcurrentMessageLimit = 8;
        }


        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<SubmitOrderConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r=>r.Intervals(100,200,500,800,1000));
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}