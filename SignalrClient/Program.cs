using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
              .WithAutomaticReconnect()
              .WithUrl("http://localhost:5000/dataforwardhub?devicecode=DPRWI00152")
              .Build();

await connection.StartAsync();



connection.On<string>("ReceiveMessage", message =>
{
    Console.WriteLine(message);
});
Console.ReadKey();

