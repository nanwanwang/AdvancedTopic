// using System.Reflection;
// using EasyNetQ;
// using EasyNetQ.AutoSubscribe;
//
// namespace EasyNewQWebClient;
//
// public static class Extensions
// {
//     
//   private static void InternalInitEasyNetQ(IServiceCollection service, string rabbitMqConnection)
//         {
//             var bus = RabbitHutch.CreateBus(rabbitMqConnection);
//             service.AddSingleton<IBus>(bus);
//             service.AddSingleton<IAutoSubscriberMessageDispatcher, WeatherForecastDispatcher>(serviceProvider => new WeatherForecastDispatcher(serviceProvider, serviceProvider.GetRequiredService<ILogger<WeatherForecastDispatcher>>()));
//
//             // var consumerTypes = Assembly.GetExecutingAssembly().GetTypes()
//             //     .Where(x => x.IsClass && !x.IsAbstract && !x.IsInterface)
//             //     .Where(x => x.BaseType.Name == typeof(EasyNetQConsumerBase<>).Name ||
//             //                 x.GetInterfaces().Any(t => t.Name == typeof(IConsume<>).Name));
//             //
//             // foreach (var consumerType in consumerTypes)
//             // {
//             //     service.AddTransient(consumerType);
//             //     //service.AddTransient(typeof(IConsume<>), consumerType);
//             // }
//             //
//             // var consumerAsyncTypes = typeof(OrderCreatedEventConsumer).Assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && !x.IsInterface)
//             //     .Where(x => x.GetInterfaces().Any(t => t.Name == typeof(IConsumeAsync<>).Name));
//             //
//             // foreach (var consumerAsyncType in consumerAsyncTypes)
//             // {
//             //     service.AddTransient(consumerAsyncType);
//             //     //service.AddTransient(typeof(IConsumeAsync<>), consumerAsyncType);
//             // }
//         }
//
//         public static void AddEasyNetQ(this IServiceCollection service, Func<string> getRabbitMqConneciton)
//         {
//             InternalInitEasyNetQ(service, getRabbitMqConneciton());
//         }
//
//         public static void AddEasyNetQ(this IServiceCollection service, string rabbitMqConnectionString)
//         {
//             InternalInitEasyNetQ(service, rabbitMqConnectionString);
//         }
//
//         public static void UseEasyNetQ(this IApplicationBuilder app)
//         {
//             var bus = app.ApplicationServices.GetRequiredService<IBus>();
//             var autoSubscriber = new AutoSubscriber(bus, "consumer")
//             {
//                 
//                 AutoSubscriberMessageDispatcher = new WeatherForecastDispatcher(app.ApplicationServices)
//                 //app.ApplicationServices.GetRequiredService<IAutoSubscriberMessageDispatcher>(),
//                 //GenerateSubscriptionId = x => AppDomain.CurrentDomain.FriendlyName + x.ConcreteType.Name,
//                // ConfigureSubscriptionConfiguration = x => x.WithAutoDelete(true).WithDurable(true)
//             };
//             
//             autoSubscriber.Subscribe(new Assembly[] {Assembly.GetExecutingAssembly()});
//             autoSubscriber.SubscribeAsync(new Assembly[] {Assembly.GetExecutingAssembly()});
//         }
//     
// }