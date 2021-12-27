using System.Text;
using Nacos.V2;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace NacosConsoleSample;

public class EventListener:IEventListener
{
    public Task OnEvent(IEvent @event)
    {
        if (@event is Nacos.V2.Naming.Event.InstancesChangeEvent e)
        {
            Console.WriteLine($"===========收到服务的变更事件============={JsonSerializer.Serialize(e)}");
        }

        return Task.CompletedTask;
    }
}