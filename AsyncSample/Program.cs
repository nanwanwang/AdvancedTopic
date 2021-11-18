// See https://aka.ms/new-console-template for more information
using AsyncSample;
using System;
//新建cts注册取消执行的回调方法 
var cts = new CancellationTokenSource();
cts.Token.Register(() => Console.WriteLine("线程取消了"));
//延迟取消 如果200没有完成调用 任务会被取消并且产生异常
cts.CancelAfter(200);
await CancellationTokenSample.GetWeatherAsync(cts.Token);
Console.ReadKey();

