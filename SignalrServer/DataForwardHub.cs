using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace SignalrServer
{
    public class DataForwardHub : Hub
    {
        public static ConcurrentDictionary<string ,string> ConnectClients = new ConcurrentDictionary<string, string>();

        
        public override async Task OnConnectedAsync()
        {
            var deviceCode = Context.GetHttpContext()?.Request.Query["devicecode"];
            ConnectClients.TryAdd(deviceCode??string.Empty,Context.ConnectionId);

            Console.WriteLine($"{deviceCode} 建立连接");
            await base.OnConnectedAsync();
            for (int i = 0; i < 1000; i++)
            {
                await SendMessage(new MessageModel 
                {
                  User= "user"+i,
                   IsOwnMessage=true,
                    IsSystemMessage=true,
                     Message="message"+i
                });
                await Task.Delay(1000);
            }
            
          
        }


        public Task SendMessage(MessageModel message)
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
