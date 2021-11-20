
using NetworkingClientSample;
using System.Diagnostics;
using System.Net.Sockets;

var sw = new Stopwatch();
sw.Start();
Console.WriteLine(sw.Elapsed);
try
{
   await  ConnectAsync();
}
catch (OperationCanceledException ex)
{
    Console.WriteLine(sw.Elapsed);

    Console.WriteLine("连接超时");
    Console.WriteLine(ex.Message);
}
catch(SocketException ex)
{
    Console.WriteLine("socket连接异常:"+ex.Message);
}

sw.Stop();

Console.ReadKey();


static async Task ConnectAsync()
{
    try
    {
        await Client.ConnectServerAsync(1);

    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
    }
}