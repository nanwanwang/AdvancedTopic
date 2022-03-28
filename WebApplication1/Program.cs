using System.Data;
using System.Data.Common;
using IoTSharp.Data.Taos;
using Microsoft.EntityFrameworkCore;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var username = "admin";
var password = "admin";
var server = "localhost";
var vhost = "/";

// string database = "db_" + DateTime.Now.ToString("yyyyMMddHHmmss");
var taosBuilder = new TaosConnectionStringBuilder()
{
    DataSource = "master",
    Port = 6030,
    Username = "root",
    Password = "taosdata"
};
// System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
// DbProviderFactories.RegisterFactory("TDengine",  TaosFactory.Instance);
builder.Services.AddTransient(provier => new TaosConnection(taosBuilder.ConnectionString));

//using var context = new WebDbContext(new DbContextOptionsBuilder().UseTaos(taosBuilder.ConnectionString).Options);

//context.Database.EnsureCreated();

// for (int i = 0; i < 10; i++)
// {
//     var rd = new Random();
//     context.Sensor.Add(new Sensor() { ts = DateTime.Now.AddMilliseconds(i + 10), degree = rd.NextDouble(), pm25 = rd.Next(0, 1000) });
//     Thread.Sleep(10);
// }
//
//  context.SaveChanges();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterEasyNetQ($"amqp://{username}:{password}@{server}/{vhost}");

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();