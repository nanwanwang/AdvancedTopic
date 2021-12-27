

using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using CastleProxy;


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



