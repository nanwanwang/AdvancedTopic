using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingClientSample;

internal class Client
{
    public static async Task<Socket> ConnectServerAsync(int timeout)
    {

       // TcpClient
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        var tokenSource = new CancellationTokenSource();
        tokenSource.CancelAfter(100);
        //tokenSource.Token.ThrowIfCancellationRequested();
        using (tokenSource.Token.Register(() =>
        {
            socket.Close();
        }))

            try
            {
                await socket.ConnectAsync(new IPEndPoint(IPAddress.Parse("192.168.1.32"), 5000), tokenSource.Token);
                Console.WriteLine("连接成功");
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("取消操作:"+ ex.Message);
            }
            catch (SocketException ex)
            {
                Console.WriteLine("socket连接异常:" + ex.Message);
            }

        return socket;
    }
}

