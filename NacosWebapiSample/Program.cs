var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((context, configurationBuilder) =>
{
    var config = configurationBuilder.Build();
    configurationBuilder.AddNacosV2Configuration(config.GetSection("NacosConfig"));
    // 也可以按需使用ini或yaml的解析器
    // builder.AddNacosV2Configuration(c.GetSection("NacosConfig"), Nacos.IniParser.IniConfigurationStringParser.Instance);
    // builder.AddNacosV2Configuration(c.GetSection("NacosConfig"), Nacos.YamlParser.YamlConfigurationStringParser.Instance);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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