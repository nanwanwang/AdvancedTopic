using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Https_SignalrServer
{
    public class DataForwardHub : Hub
    {
        public static ConcurrentDictionary<string ,string> ConnectClients = new ConcurrentDictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            var deviceCode = Context.GetHttpContext()?.Request.Query["devicecode"];
            ConnectClients.TryAdd(deviceCode??string.Empty,Context.ConnectionId);
            Console.WriteLine($"{deviceCode}连接,connectionid=>{Context.ConnectionId}");
            await base.OnConnectedAsync();
            await SendMessage("hahaa");
        }


        public Task SendMessage(string message)
        {
            return Clients.Client(ConnectClients["DPRWI00152"]).SendAsync("ReceiveMessage",message);
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if(ConnectClients.Values.Contains(Context.ConnectionId))
            {
                Console.WriteLine(Context.ConnectionId);
            }
            return base.OnDisconnectedAsync(exception);
        }


        public void DoWithMessage(string message)
        {
            Console.WriteLine(message);
        }

    }
}
