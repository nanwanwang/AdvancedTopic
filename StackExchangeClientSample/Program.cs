using StackExchangeClientSample;
using StackExchangeShared;
using StackExchangeShared.Serialization;
using StackExchangeShared.Streaming;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSerialization().AddRedis(builder.Configuration)
    .AddRedisStreaming().AddHostedService<Worker>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();