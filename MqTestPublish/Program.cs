using EasyNetQ;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var username = "admin";
var password = "admin";
var server = "localhost";
var vhost = "/";

//using var bus = RabbitHutch.CreateBus($"amqp://{username}:{password}@{server}/{vhost}");

builder.Services.RegisterEasyNetQ($"amqp://{username}:{password}@{server}/{vhost}");
// builder.Services.AddMassTransit(config =>
// {
//     
//     config.UsingRabbitMq((context, cfg) =>
//     {
//         
//         cfg.ConfigureEndpoints(context);
//         cfg.Host(new Uri("rabbitmq://localhost"), h =>
//          {
//              h.Username("admin");
//              h.Password("admin");
//          });
//     });
// });
// builder.Services.AddOptions<MassTransitHostOptions>().Configure(options =>
// {
//     options.WaitUntilStarted = true;
//     options.StartTimeout = TimeSpan.FromSeconds(10);
//     options.StopTimeout = TimeSpan.FromSeconds(30);
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();