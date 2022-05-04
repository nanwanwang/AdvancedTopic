using Exceptionless;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionless(configuration =>
{
    configuration.ServerUrl = "http://localhost:5000";
    configuration.ApiKey = "5lWNktyHIQynu6TLZiZltgnLgedXtOSoE9xcJNgn";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseExceptionless();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", async _ =>
{
    var exception = new Exception("haha");
    exception.ToExceptionless().Submit();
    await _.Response.WriteAsync("hah");
});

app.Run();