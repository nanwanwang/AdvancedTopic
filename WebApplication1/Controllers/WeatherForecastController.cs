using System.Data;
using EasyNetQ;
using EasyNetQModel;
using IoTSharp.Data.Taos;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IBus _bus;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly TaosConnection _dbConnection;

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IBus bus, TaosConnection dbConnection)
    {
        _logger = logger;
        _bus = bus;
        _dbConnection = dbConnection;
    }

    [HttpGet("createdb")]
    public async Task<int> CreateDb(string dbName)
    {
        await using (_dbConnection)
        {
            await _dbConnection.OpenAsync();
            var command = _dbConnection.CreateDbCommand(dbName, true);
            return await command.ExecuteNonQueryAsync();
        }
        
    }

    [HttpGet("dropdb")]
    public async Task<int> DropDb(string dbName)
    {
        await using (_dbConnection)
        {
            await _dbConnection.OpenAsync();
            return  await  _dbConnection.DropDbCommand(dbName).ExecuteNonQueryAsync();
        }
    }

    [HttpGet("createtable")]
    public async Task<int> CreateTable(string dbName, string tableName, string tableSchema)
    {
        await using (_dbConnection)
        {
            await _dbConnection.OpenAsync();
            var command = _dbConnection.CreateTableCommand(dbName, tableName, tableSchema, true);
            return await command.ExecuteNonQueryAsync();
        }
    }

    
    [HttpPost("insertdata")]
    public async Task<int> Insert(string dbName,string tableName,List<float> floatValues)
    {
        await using (_dbConnection)
        {
            await _dbConnection.OpenAsync();
            var command = _dbConnection.InsertFloorTagsCommand(dbName, tableName, floatValues);
            return await command.ExecuteNonQueryAsync();
        }
    }
    [HttpPost("querydata")]
    public async Task<List<TestModel>> Select(string dbName, string tableName, string queryExpression)
    {
        await using (_dbConnection)
        {
            await _dbConnection.OpenAsync();
            var command = _dbConnection.SelectCommand(dbName, tableName, queryExpression);
            var reader= await command.ExecuteReaderAsync();
            var result= reader.ToJson<List<TestModel>>();
            return result;
        }
    }

    [HttpPost("CreateSupperTable")]
    public async Task<int> CreateSupperTable(string dbName, string tableName,string tableSchema,string tagsSchema)
    {
        await using (_dbConnection)
        {
            await _dbConnection.OpenAsync();
            var command = _dbConnection.CreateSupperTableCommand(dbName, tableName, tableSchema,tagsSchema);
            return await command.ExecuteNonQueryAsync();
        }
    }

    [HttpPost("CreateSupperTableSub")]
    public async Task<int> CreateSupperTableSub(string dbName,  string tableName,string sTableName,string tagsValue)
    {
        await using (_dbConnection)
        {
            await _dbConnection.OpenAsync();
            var command = _dbConnection.CreateTableBySupperTableCommand(dbName, tableName, sTableName,tagsValue);
            return await command.ExecuteNonQueryAsync();
        }
    }
    


    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    { 
       // await _bus.PubSub.PublishAsync(new TextMessage() {Text = "WeatherForecast"}, "A.B");
       //_dbConnection.Open();
      // Console.WriteLine(_dbConnection.ServerVersion);
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}