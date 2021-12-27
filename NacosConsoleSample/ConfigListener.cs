using Nacos.V2;

namespace NacosConsoleSample;

public class ConfigListener:IListener 
{
    public void ReceiveConfigInfo(string configInfo)
    {
        Console.WriteLine($"==============收到配置变更信息了================> {configInfo}"); 
    }
}