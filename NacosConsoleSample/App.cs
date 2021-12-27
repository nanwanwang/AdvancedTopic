using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Nacos.V2;

namespace NacosConsoleSample;

public class App
{
    private readonly ILogger<App> _logger;

    private readonly INacosConfigService _ns;
    public App(ILogger<App> logger, INacosConfigService ns)
    {
        _logger = logger;
        _ns = ns;
    }

    public async Task RunAsync(string[] args)
    {
        await PublishConfig(_ns);
        await GetConfig(_ns);
        //await RemoveConfig(_ns);
    }

    static async Task PublishConfig(INacosConfigService svc)
    {
        var dataId = "demo-dateid";
        var group = "demo-group";
        var val = "test-value-" + DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        await Task.Delay(500);
        var flag = await svc.PublishConfig(dataId, group, val);
        Console.WriteLine($"=============发布配置结束,{flag}");
    }

    static async Task GetConfig(INacosConfigService svc)
    {
        var dataId = "demo-dateid";
        var group = "demo-group";

        await Task.Delay(500);

        var config = await svc.GetConfig(dataId, group, 5000L);

        Console.WriteLine($"=============获取配置结果,{config}");
    }

    static async Task RemoveConfig(INacosConfigService svc)
    {
        var dataId = "demo-dateid";
        var group = "demo-group";

        await Task.Delay(500);

        var flag = await svc.RemoveConfig(dataId, group);
        Console.WriteLine($"==================删除配置结果,{flag}");
    }
    
    
}