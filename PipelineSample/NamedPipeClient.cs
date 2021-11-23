using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineSample
{
    internal class NamedPipeClient
    {
        public static async Task StartAsync()
        {
            using var client = new NamedPipeClientStream("testpipe");
            Console.Write("Attempting to  connect to pipe...");

            await client.ConnectAsync();

            Console.WriteLine("connected to pipe.");
            Console.WriteLine($"There are currently {client.NumberOfServerInstances} pipe server instances open.");

            using var sr = new StreamReader(client);
            string temp;
            while ((temp = sr.ReadLine()) != null)
            {
                Console.WriteLine($"Received from server :{temp}");
            }

        }
    }
}
