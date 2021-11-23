

// await TrySomethingAsync();
await DoSomethingAsync();
Console.WriteLine("hello demon");
//Console.ReadKey();


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
