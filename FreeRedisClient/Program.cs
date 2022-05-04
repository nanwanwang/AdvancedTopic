// See https://aka.ms/new-console-template for more information


using FreeRedis;

var redisClient = new RedisClient(new ConnectionStringBuilder()
{
    Host = "127.0.0.1",
     
})
{
    Serialize = obj => System.Text.Json.JsonSerializer.Serialize(obj),
    Deserialize = (json, type) => System.Text.Json.JsonSerializer.Deserialize(json, type)
};
redisClient.Connected += (sender, eventArgs) =>
{
    eventArgs.Client.Subscribe("temp", OnDataReceived);
};
redisClient.con
redisClient.Unavailable += (sender, eventArgs) =>
{
   var client= sender as RedisClient;
   client?.UnSubscribe("temp");
};

//redisClient.Subscribe("temp", OnDataReceived);




Console.ReadLine();
 static void OnDataReceived(string channel, object o)
{
    Console.WriteLine(o);
}

