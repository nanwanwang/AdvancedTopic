using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, configurator) =>
    {

        configurator.ConfigureEndpoints(context);
        configurator.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("admin");
            h.Password("admin");
        });
    });
});

builder.Services.AddOptions<MassTransitHostOptions>().Configure(options =>
{
    options.WaitUntilStarted = true;
    options.StartTimeout = TimeSpan.FromSeconds(10);
    options.StopTimeout = TimeSpan.FromSeconds(30);
});

var app = builder.Build();



app.MapGet("/", () => "Hello World!");

app.Run();