using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;

namespace MassTransitWorker1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        
                        x.SetKebabCaseEndpointNameFormatter();

                        // By default, sagas are in-memory, but should be changed to a durable
                        // saga repository.
                       // x.SetInMemorySagaRepositoryProvider();

                        var entryAssembly = Assembly.GetEntryAssembly();

                        x.AddConsumer<SubmitOrderConsumer>(typeof(SubmitOrderConsumerDefinition));
                        x.AddConsumer<ValueEnteredEventConsumer>();
                        //     .Endpoint(e =>
                        // {
                        //     e.Name = "order-service-extreme";
                        //     e.Temporary = false;
                        //     e.ConcurrentMessageLimit = 8;
                        //     e.PrefetchCount = 16;
                        //     e.InstanceId = "something-unique";
                        // });
                       // x.AddSagaStateMachines(entryAssembly);
                       // x.AddSagas(entryAssembly);
                        //x.AddActivities(entryAssembly);

                      //  x.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });
                      x.UsingRabbitMq((context, configurator) =>
                      {
                          configurator.ConfigureEndpoints(context);
                          configurator.Host(new Uri("rabbitmq://localhost"), config =>
                          {
                              config.Username("admin");
                              config.Password("admin");
                          });
                      } );
                    });
                });
    }
}