using System.Threading.Channels;

namespace EasyNewQWebClient;

public class SubscribeRequestsChannel
{
    public readonly Channel<ISubscribeRequest> Requests = Channel.CreateUnbounded<ISubscribeRequest>();
}