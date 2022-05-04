using StackExchangeShared;
using StackExchangeShared.Serialization;
using StackExchangeShared.Streaming;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerialization().AddRedis(builder.Configuration)
    .AddRedisStreaming();
    

var app = builder.Build();
var count = 0;

app.MapGet("/", async () =>
{
    var streamPublisher = app.Services.GetService<IStreamPublisher>();
    await streamPublisher.PublishAsync("12", "hahah_"+count++);
});


app.Run();