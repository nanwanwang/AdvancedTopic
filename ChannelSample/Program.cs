using ChannelSample;
using System.Threading.Channels;

var channelOptions = new BoundedChannelOptions(3) { FullMode = BoundedChannelFullMode.Wait };
var channel = Channel.CreateBounded<int>(channelOptions);

_ = Task.Factory.StartNew(async () =>
 {
     await Task.Delay(5000);
     var result = await channel.Reader.ReadAsync();
     Console.WriteLine(result);
 }).ConfigureAwait(false);

await channel.Writer.WriteAsync(1);
await channel.Writer.WriteAsync(2);
await channel.Writer.WriteAsync(3);

if(await channel.Writer.WaitToWriteAsync()) 
    await channel.Writer.WriteAsync(4);

await foreach (var item in channel.Reader.ReadAllAsync())
{
    Console.WriteLine(item);
}



var p = new Person { Name = "demon", Id = 1, Age = 17 };

Console.WriteLine(p);

await SimpleChannelSample.ChannelTest();


//await SimpleChannelSample.ChannelTestAsync();


