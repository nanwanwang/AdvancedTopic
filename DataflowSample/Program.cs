using DataflowSample;

var dataPipeline = new CommonDataPipeline<string>("demon");

dataPipeline.AddAction((data) => Console.WriteLine(data+"1"));
dataPipeline.AddAction((data) => Console.WriteLine(data+"2"));
dataPipeline.AddAction((data) => Console.WriteLine(data+"3"));

dataPipeline.Invoke();

Console.ReadLine();