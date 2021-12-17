
using System.Net.Sockets;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using RecordSample;

//record
var p = new Person() { Name = "demon", Age = 17 };
var p2 = p with { Age = 18 };
var p3 = new Person() { Name = "demon", Age = 17 };
var h4 = new Human("Jack", 12);
Console.WriteLine(p);
Console.WriteLine(p2);
Console.WriteLine(p==p3);
Console.WriteLine(h4);
Console.WriteLine(h4.Name+","+h4.Age);

// null-forgiving operator
var obj = JObject.Parse("{\"name\":\"demon\"}");

var jObj = new JObject(new JProperty("name", "demon"));
var jObj2 = new JObject(new JProperty("address", new JObject(new JProperty("region", "huangpu"))));
Console.WriteLine(jObj);
Console.WriteLine(jObj2);
var name = obj["name"]!.Value<string>();
var name2 = jObj["name"]!.Value<string>();
Console.WriteLine(name);
Console.WriteLine(name2);

Console.ReadKey();