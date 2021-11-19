// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!!!");
int result =await  SomeValueTaskReturningMethodAsync();
int result2 = await SomeValueTaskReturningMethodSync();

Console.WriteLine(result);
Console.WriteLine(result2); 
//int result1 = await SomeValueTaskReturningMethodAsync().ConfigureAwait(false);

//Task<int> t = SomeValueTaskReturningMethodAsync().AsTask();

//Console.WriteLine(await t.ConfigureAwait(false));
// await AccessRubiksCodeAsync();


var listOfInts = new List<int>() { 1, 2, 3 };


//foreach (var item in listOfInts)
//{
//    await WaitThreeSeconds(item);
//}

var tasks = new List<Task>();
foreach (var item in listOfInts)
{
    tasks.Add(WaitThreeSeconds(item));
}

Task.WhenAll(tasks);


Console.ReadKey();

//使用ValueTask<T> 可以是同步的 也可以是异步的  在调用的时候都可以使用await等待
//同步代码
static  ValueTask<int> SomeValueTaskReturningMethodSync()
{

    return  new ValueTask<int>(2);
}

//异步代码
static async ValueTask<int> SomeValueTaskReturningMethodAsync()
{
    await Task.Delay(100);
    return 1;
}

static async Task<int> AccessRubiksCodeAsync()
{
    HttpClient client = new HttpClient();
    var getContent = client.GetStringAsync("http://www.baidu.com");
    LogToConsole("Yay!");

    string content = await getContent;

    Console.WriteLine(content.Length);
    return content.Length;

}

static async Task WaitThreeSeconds(int param)
{
    Console.WriteLine($"{param} started ---({DateTime.Now:hh:mm:ss})----");
    await Task.Delay(3000);
    Console.WriteLine($"{param} finished ---({DateTime.Now:hh:mm:ss})----");
}

static void LogToConsole(string message)
{
    Console.WriteLine("At the moment I am actually listening to the new NIN song…");
    Console.WriteLine("It is pretty cool…like something of David Bowie's – Blackstar.");
    Console.WriteLine("message");
}
