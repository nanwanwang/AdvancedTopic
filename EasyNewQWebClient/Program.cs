using System.ComponentModel.Design;
using System.Reflection;
using EasyNetQ.AutoSubscribe;
using EasyNewQWebClient;

var builder = WebApplication.CreateBuilder(args);

var username = "admin";
var password = "admin";
var server = "localhost";
var vhost = "/";

builder.Services.RegisterEasyNetQ($"amqp://{username}:{password}@{server}/{vhost}");

builder.Services.AddSingleton<SubscribeRequestsChannel>();
// builder.Services.AddTransient(typeof(StringConsumer));
// builder.Services.AddTransient(typeof(WeatherForecastConsumer));
builder.Services.AddTransient(typeof(PublishTagMessageConsumer));
builder.Services.AddTransient(typeof(NormalChargingConsumer));
builder.Services.AddSingleton<IAutoSubscriberMessageDispatcher, WeatherForecastDispatcher>(serviceProvider => new WeatherForecastDispatcher(serviceProvider));
builder.Services.AddHostedService<Worker>();

var app = builder.Build();


app.UseSubscribe("EasyNetQDemo", new Assembly[] {Assembly.GetExecutingAssembly()});
    
app.MapGet("/", () => "Hello World!");
app.MapPost("/easymq/startsubscribe", async(SubscribeRequestsChannel channel)=>
{
     await channel.Requests.Writer.WriteAsync(new StartSubscribe());
     return Results.Ok();
});
app.MapPost("/easymq/stopsubscribe", async (SubscribeRequestsChannel channel) =>
{
    await channel.Requests.Writer.WriteAsync(new StopSubscribe());
    return Results.Ok();
});
app.Run();