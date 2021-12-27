using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nacos.V2.DependencyInjection;
using NacosConsoleSample;

var services = new ServiceCollection();
services.AddNacosV2Config(options =>
{
    options.ServerAddresses = new List<string>() { "http://192.168.1.207:8848/" };
    options.EndPoint = "";
    options.Namespace = "cs-test";
    options.ConfigUseRpc = true;
});
services.AddNacosV2Naming(options =>
{
    options.ServerAddresses = new List<string>() { "http://192.168.1.207:8848" };
    options.EndPoint = "";
    options.Namespace = "cs-test";
    options.NamingUseRpc = true;
});
services.AddTransient<App>();
var serviceProvider = services.BuildServiceProvider();
var service =  ActivatorUtilities.CreateInstance<App>(serviceProvider);

await  service.RunAsync(args);

//var host = AppStartup();
//var service = ActivatorUtilities.CreateInstance<App>(host.Services);

//await service.RunAsync(args);

static IHost AppStartup()
{
    return Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            ConfigureServices(context, services);
            services.AddTransient<App>();
        }).Build();
}

static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    services.AddNacosV2Config(options =>
    {
        options.ServerAddresses = new List<string>() { "http://192.168.1.207:8848/" };
        options.EndPoint = "";
        options.Namespace = "cs-test";
        options.ConfigUseRpc = true;
    });

    services.AddNacosV2Naming(options =>
    {
        options.ServerAddresses = new List<string>() { "http://192.168.1.207:8848/" };
        options.EndPoint = "";
        options.Namespace = "cs-test";
        options.NamingUseRpc = true;
    });
}