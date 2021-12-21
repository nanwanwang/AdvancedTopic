using Autofac.Extensions.DependencyInjection;
using MinimalApi;
#region direct create app
//var app = WebApplication.Create(args);
//app.MapGet("/", () => "hello world!");
//app.Run();
#endregion

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath =Directory.GetCurrentDirectory(),
    EnvironmentName =Environments.Staging,
    WebRootPath ="customwwwroot"
});


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//add file config
builder.Configuration.AddJsonFile("appsettings.json");
//add log provider
//builder.Logging.AddJsonConsole();

builder.Services.AddMemoryCache();

//customize the ihostbuilder
// wait 30s for graceful shutdown
builder.Host.ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(30));

//customize the iwebhostbuilder
#if windows
builder.WebHost.UseHttpSys();
#endif

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddScoped<SampleService>();

var app = builder.Build();

//add multiple ports
//app.Urls.Add("http://localhost:3000");
//app.Urls.Add("http://localhost:4000");
//add https url
//app.Urls.Add("https://localhost:5500");
app.MapControllers();

app.UseFileServer();
//read environment
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/oops");
}

//app.MapGet("/inline", () => "this is an inline lambda");
//var handler = () => "this is a lambda variable";
//app.MapGet("/", handler);
//app.MapGet("/oops", () => "Oops! An error happened");
////app.MapGet("/", () => "This is a GET");
//app.MapPost("/", () => "This is a POST");
//app.MapPut("/", () => "This is a PUT");
//app.MapDelete("/", () => "This is a DELETE");

//app.MapMethods("/options-or-head", new[] { "OPTIONS", "HEAD" },
//                          () => "This is an options or head request ");

//app.MapGet("/hello", () => "hello named route").WithName("hi");

//app.MapGet("/", (LinkGenerator linker) => $"The link to the hello route is {linker.GetPathByName("hi", values: null)}");

app.MapGet("/users/{userId}/books/{bookId}", (int userId, int bookId) => $"The user id is{userId} and book is {bookId}");

using var scope= app.Services.CreateScope();
var sampleService = scope.ServiceProvider.GetService<SampleService>();
app.Logger.LogInformation($"{builder.Environment.ApplicationName}");
app.Logger.LogInformation($"{builder.Environment.ContentRootPath}");
app.Logger.LogInformation($"{builder.Environment.EnvironmentName}");
app.Logger.LogInformation($"{builder.Environment.WebRootPath}");

app.Logger.LogError(sampleService!.DoSomething());
//write log
app.Logger.LogInformation("The app started");
//configuration
var message = app.Configuration["Hellokey"] ?? "hello";

app.Run();
//specify port
//app.Run("http://*:5000");

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
