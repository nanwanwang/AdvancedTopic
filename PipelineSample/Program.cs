using PipelineSample;
using System.IO.Pipes;


await Task.Factory.StartNew(async () => await NamedPipeServer.StartAsync());

await NamedPipeClient.StartAsync();

Console.ReadLine();

