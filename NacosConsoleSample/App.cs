using System.Text.Json;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Nacos.V2;
using Nacos.V2.Naming.Dtos;

namespace NacosConsoleSample;

public class App
{
    private readonly ILogger<App> _logger;
    ConfigListener listener = new ConfigListener();
    private EventListener _listener = new();
    private readonly INacosConfigService _ns;
    private readonly INacosNamingService _nacosNaming;
    public App(ILogger<App> logger, INacosConfigService ns, INacosNamingService nacosNaming)
    {
        _logger = logger;
        _ns = ns;
        _nacosNaming = nacosNaming;
    }

    public async Task RunAsync(string[] args)
    {
        await PublishConfig(_ns);
        await GetConfig(_ns);
        //await RemoveConfig(_ns);
        await ListenConfig(_ns, listener);

        // await RegisterInstance(_nacosNaming, 9997);
        //
        // await GetAllInstance(_nacosNaming);
        //
        // await DeRegisterInstance(_nacosNaming);
        await  Subscribe(_nacosNaming,_listener);
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

    static async Task ListenConfig(INacosConfigService svc, IListener listener)
    {
        var dataid = "demo-dateid";
        var group = "demo-group";
        await svc.AddListener(dataid, group, listener);

        await GetConfig(svc);
    }

    static async Task RegisterInstance(INacosNamingService svc, int port = 9999)
    {
        var instance = new Instance()
        {
            ServiceName = "demo-svc1",
            ClusterName = Nacos.V2.Common.Constants.DEFAULT_CLUSTER_NAME,
            Ip = "192.168.1.207",
            Port = port,
            Enabled = true,
            Ephemeral = true,
            Healthy = true,
            Weight = 100,
            InstanceId = $"demo-svc1-192.168.1.207-{port}",
            Metadata = new Dictionary<string, string>()
            {
                { "m1", "v1" },
                { "m2", "v2" }
            }
        };
        await svc.RegisterInstance(instance.ServiceName, Nacos.V2.Common.Constants.DEFAULT_GROUP, instance);
        Console.WriteLine($"======================注册实例成功");
    }

    static async Task GetAllInstance(INacosNamingService svc)
    {
        var list = await svc.GetAllInstances("demo-svc1", Nacos.V2.Common.Constants.DEFAULT_GROUP, false);

        Console.WriteLine($"获取到所有注册的服务信息{JsonSerializer.Serialize(list)}");
    }

    static async Task DeRegisterInstance(INacosNamingService svc)
    {
        await svc.DeregisterInstance("demo-svc1", Nacos.V2.Common.Constants.DEFAULT_GROUP, "192.168.1.207", 9997);
        Console.WriteLine("===========注销所有服务成功==================");
    }


    static async Task Subscribe(INacosNamingService svc, IEventListener listener)
    {
        await svc.Subscribe("demo-svc1", Nacos.V2.Common.Constants.DEFAULT_GROUP, listener);

        await RegisterInstance(svc, 9997);

        await Task.Delay(3000);

        await svc.Unsubscribe("demo-svc1", Nacos.V2.Common.Constants.DEFAULT_GROUP, listener);
        
        await RegisterInstance(svc);

        await Task.Delay(1000);
    }
    
}