// See https://aka.ms/new-console-template for more information
using AsyncSample;
using System;

#region 1. CancellationTokenSource

#region 线程取消 

//////新建cts注册取消执行的回调方法 
//var cts = new CancellationTokenSource();
//cts.Token.Register(() => Console.WriteLine("线程取消了"));
//////延迟取消 如果200没有完成调用 任务会被取消并且产生异常
//cts.CancelAfter(200);
////await CancellationTokenSample.GetWeatherAsync(cts.Token);



//var cts5 = new CancellationTokenSource();
//cts5.Token.Register(() => Console.WriteLine("线程取消了"));
//cts5.Cancel();
//var result = await CancellationTokenSample.OperationAsync(cts5.Token);

//Console.WriteLine("waiting ...");

//Console.WriteLine(result);

#endregion

#region  关联取消
// 无论cts1 还是cts2 被取消 cts3都会被取消
//var cts1 = new CancellationTokenSource();
//var cts2 = new CancellationTokenSource();   
//var cts3 = CancellationTokenSource.CreateLinkedTokenSource(cts1.Token, cts2.Token);

//cts1.Token.Register(() => Console.WriteLine("cts1 Canceling"));
//cts2.Token.Register(() => Console.WriteLine("cts2 Canceling"));

//cts2.CancelAfter(200);
//cts3.Token.Register(() => Console.WriteLine("root Canceling"));
//await CancellationTokenSample.GetWeatherAsync2(cts1.Token,cts2.Token,cts3.Token);


#endregion

#region 判断取消
//var cts4 = new CancellationTokenSource();
//cts4.Token.Register(() => Console.WriteLine("cts4取消"));
//cts4.CancelAfter(5000);

//await CancellationTokenSample.LongRunningAsync(cts4.Token);

#endregion

#endregion


#region 2. Task
//await TaskSample.RunWorkFlow();

//await TaskSample.RunWorkFlowParallel();
await TaskSample.RunWorkFlowWhenAny();
#endregion



Console.ReadKey();

