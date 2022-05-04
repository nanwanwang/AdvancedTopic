// See https://aka.ms/new-console-template for more information

var client = new HttpClient();
var str = await client.GetStringAsync("https://google.com");


Console.ReadLine();