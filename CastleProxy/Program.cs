

using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using CastleProxy;
using Microsoft.Extensions.DependencyInjection;
using AppContext = CastleProxy.AppContext;


//aspect sample
var services = new ServiceCollection();
services.AddScoped<ICustomService, CustomService>();
services.AddScoped<ICustomServiceWithArgs, CustomServiceWithArgs>();
services.ConfigureDynamicProxy(configuration =>
{
    // 全局方式 注入 interceptor  可以过滤需要注入的服务
    configuration.Interceptors.AddTyped<CustomInterceptorAttribute>(method =>
        method.DeclaringType.Name.EndsWith("Service"));
    //configuration.Interceptors.AddTyped<CustomInterceptorWithArgsAttribute>(new object[] { "custom" });
    
    // 添加全局忽略  跟在对应的类或接口上加[NonAspect]效果一样
    configuration.NonAspectPredicates.AddMethod("Call");
} );
var serviceProvider = services.BuildDynamicProxyProvider();

var service = serviceProvider.GetRequiredService<ICustomService>();
//var service = ActivatorUtilities.CreateInstance<CustomService>(serviceProvider);
service.Call();
var customServiceWithArgs = serviceProvider.GetRequiredService<ICustomServiceWithArgs>();
customServiceWithArgs.Call();

// mvc action sample
var appContext = new AppContext();
// object rs = appContext.Process("Test", "HelloWorld", "demon");
// Console.WriteLine($"process 执行结果1:{rs}");
// Console.WriteLine("=".PadRight(50, '='));
appContext.AddExecRouteTemplate("{controller}/{action}/{name}");
appContext.AddExecRouteTemplate("{action}/{controller}/{name}");
object result1 = appContext.Run("HelloWorld/Test/demon");
Console.WriteLine($"执行的结果1：{result1}");
 
Console.WriteLine("=".PadRight(50, '='));
 
object result2 = appContext.Run("Test/HelloWorld/demon");
Console.WriteLine($"执行的结果2：{result2}");

Console.WriteLine("=".PadRight(50, '='));
 
appContext.AddExecRouteTemplate("{action}/{controller}/{a}/{b}");
object result3 = appContext.Run("Add/Test/500/20");
Console.WriteLine($"执行的结果3：{result3}");
 
object result4 = appContext.Run("Test/TestError/demon");
Console.WriteLine($"执行的结果4：{result4}");


// basic usage
ILogger logger = new ConsoleLogger();
var product = new Product() { Name = "book" };
IProductRepository target = new ProductRepository();
ProxyGenerator generator = new ProxyGenerator();
LoggerAsyncIntercecptor asyncIntercecptor = new LoggerAsyncIntercecptor(logger);
IInterceptor loggerInterceptor = new LoggerInterceptor(logger,asyncIntercecptor);
IProductRepository proxy = generator.CreateInterfaceProxyWithTarget(target,loggerInterceptor);

// use class the methos must be virtual method
var productReposity =(ProductRepository) generator.CreateClassProxy(typeof(ProductRepository),loggerInterceptor);

await proxy.UpdateAsync(product);
await productReposity.UpdateAsync(product);
// use autofac 
ContainerBuilder builder = new();
builder.RegisterType<LoggerAsyncIntercecptor>().AsSelf();
builder.RegisterType<LoggerInterceptor>().AsSelf();
builder.RegisterType<ConsoleLogger>().AsImplementedInterfaces();

builder.RegisterType<ProductRepository>().AsImplementedInterfaces().EnableInterfaceInterceptors()
    .InterceptedBy(typeof(LoggerInterceptor));

var container = builder.Build();

var productRepository = container.Resolve<IProductRepository>();
Product product1 = new() { Name = "book1" };
await productRepository.UpdateAsync(product1);






Console.ReadKey();



