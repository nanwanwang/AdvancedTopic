using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace ChannelSample
{
    internal class SimpleChannelSample
    {
        public static async ValueTask ChannelTest()
        {
            var myChannel = Channel.CreateUnbounded<int>();


            _ = Task.Factory.StartNew(async () =>
             {
                 for (int i = 0; i < 10000; i++)
                 {
                     await myChannel.Writer.WriteAsync(i);
                     await Task.Delay(1000);
                 }
             });

       
            while (true)
            {
                var item = await myChannel.Reader.ReadAsync();
                Console.WriteLine(item);
            }
        }

        
        
        public static async ValueTask ChannelTestAsync()
        {
            var channel = Channel.CreateUnbounded<int>();

           _= Task.Factory.StartNew(async () =>
            {

                for (int i = 0; i < 100; i++)
                {
                    await channel.Writer.WriteAsync(i);
                }
            });

            await foreach (var item in channel.Reader.ReadAllAsync())
            {
                Console.WriteLine(item);
                await Task.Delay(1000);
            }


        }



    }
}
