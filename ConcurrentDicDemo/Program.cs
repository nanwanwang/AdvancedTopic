// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;
using System.Text.Json;

var dic = new ConcurrentDictionary<string, int>();
dic.TryAdd("1", 1);
dic.TryAdd("2", 2);

var newValue = 1;
dic.AddOrUpdate("1", newValue, (key, value) => newValue);

var tag = new Tag()
{
    TagName = "tag1",
    TagValue = "222"
};

Console.WriteLine(JsonSerializer.Serialize(tag));
Console.WriteLine(tag.TagName+tag.TagValue);

foreach (var item in dic)
{
    Console.WriteLine($"{item.Key}:{item.Value}");
}



Console.ReadKey();
struct Tag
{
    public  string TagName { get; set; }
    public string TagValue { get; set; }
}

