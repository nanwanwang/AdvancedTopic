// See https://aka.ms/new-console-template for more information

using System.Data.Common;
using System.Threading.Channels;
using IoTSharp.Data.Taos;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
DbProviderFactories.RegisterFactory("TDengine",  TaosFactory.Instance);

string database = "db_normal";

var builder = new TaosConnectionStringBuilder()
{
    DataSource = "127.0.0.1",
    DataBase = database,
    Username = "root",
    Password = "taosdata",
    Port = 6030
};

using var connection = new TaosConnection(builder.ConnectionString);
connection.Open();

Console.WriteLine($"ServerVersion:{connection.ServerVersion}");
Console.WriteLine($"Create {database}{connection.CreateCommand($"create database {database}").ExecuteNonQuery()}");

Console.ReadKey();