using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace PipelineSample
{
    internal class NamedPipeServer
    {
        public static async Task StartAsync()
        {

            using var pipeServer = new NamedPipeServerStream("testpipe");
            Console.WriteLine("NamePipeServerStream object created.");


            Console.Write("waiting for client connection ...");

            await pipeServer.WaitForConnectionAsync();

            try
            {
                using var sw = new StreamWriter(pipeServer);
                sw.AutoFlush = true;
                while (true)
                {
                    Console.WriteLine("Enter text:");
                    sw.WriteLine(Console.ReadLine());
                }
            
            }
            catch (IOException e)
            {

                Console.WriteLine($"ERROR:{e.Message}");
            }
       
        }
    }
}
