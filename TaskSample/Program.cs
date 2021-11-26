using TaskSample;

// await TrySomethingAsync();
//await DoSomethingAsync();
//Console.WriteLine("hello demon");
//Console.ReadKey();

//var res = await TaskExample.DownloadStringWithRetries("http://www.google.com");

//if (res != null) Console.WriteLine(res);

//var res2 = await TaskExample.DownloadStringWithTimeout("http://www.google.com");

//if (res2 != null) Console.WriteLine(res2);

//await TaskExample.DownloadAllAsync(new List<string> { "http://www.google.com", "http://www.baidu.com" });

//await TaskExample.ObserveOneExceptionAsync();

//await TaskExample.ObserveTwoExceptionAsync();

//await TaskExample.ProcessTaskAsync();
//await TaskExample.ProcessTaskAsync2();

 TaskExample.TestAsync();

Console.ReadKey();


static async Task TrySomethingAsync()
{
   Task task =  PossibleExceptionAsync();
    try
    {
        await task;
       // await PossibleExceptionAsync();
    }
    catch (NotSupportedException ex)
    {

        throw ;
    }
  
}

static Task PossibleExceptionAsync()
{
    throw new NotSupportedException("不支持异常");
}

static async Task DoSomethingAsync()
{
    await Task.Delay(1000);
    Console.WriteLine("do something async");
}
