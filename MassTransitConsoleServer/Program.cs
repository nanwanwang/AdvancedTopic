// See https://aka.ms/new-console-template for more information


using MassTransit;
using MassTransitCommon;

var busControl = Bus.Factory.CreateUsingRabbitMq(configurator =>
{
    configurator.Host(new Uri("rabbitmq://localhost"), config =>
    {
        config.Username("admin");
        config.Password("admin");
    });
});
var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

await  busControl.StartAsync(source.Token);
int count = 0;
try
{
    while (true)
    {
        // string? value = await Task.Run(() =>
        // {
        //     Console.WriteLine("Enter message (or quit to exit)");
        //     Console.Write("> ");
        //     return
        //     Console.ReadLine();
        // });
        // if("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
        //     break;

        try
        {
            await busControl.Publish<ValueEntered>(new
            {
                Value = count++ +" valueentered"
            });
            
            await busControl.Publish<SubmitOrder>(new
            {
                OrderId = count++ + " orderid"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
           
        }
       

        

        await Task.Delay(2000);
    }
}
finally
{
    await busControl.StopAsync();
}