using IAsyncEnumerableSample;

//wait until fetchIOTData  completed 
//foreach (var item in await IEnumerableSample.FetchIOTData())
//{
//    Console.WriteLine(item);
//}


//can return one by one 
await foreach (var item in  IEnumerableSample.FetchIOTDataAsync())
{
    Console.WriteLine(item);
}


Console.ReadLine();
