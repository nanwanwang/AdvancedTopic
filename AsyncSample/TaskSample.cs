using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSample;

internal class TaskSample
{

    public static async Task RunWorkFlow()
    {
        var stopWatch = Stopwatch.StartNew();
        await Task.Delay(2000).ContinueWith(_ => Completed("Task1", stopWatch.Elapsed));
        await Task.Delay(3000).ContinueWith(_ => Completed("Task2", stopWatch.Elapsed));
        await Task.Delay(1000).ContinueWith(_ => Completed("Task3", stopWatch.Elapsed));
        Console.WriteLine("WorkFlow:" + DateTime.Now);
        stopWatch.Stop();
    }


    public static async Task RunWorkFlowParallel()
    {
        var stopWatch = Stopwatch.StartNew();
        var task1=Task.Delay(2000).ContinueWith(_ => Completed("Task1", stopWatch.Elapsed));
        var task2=Task.Delay(3000).ContinueWith(_ => Completed("Task2", stopWatch.Elapsed));
        var task3=Task.Delay(1000).ContinueWith(_ => Completed("Task3", stopWatch.Elapsed));
        await Task.WhenAll(task1, task2, task3);
        Console.WriteLine("WorkFlow:" + DateTime.Now);
        stopWatch.Stop();
    }

    public static async Task RunWorkFlowWhenAny()
    {
        var stopWatch = Stopwatch.StartNew();
        var task1 = Task.Delay(2000).ContinueWith(_ => Completed("Task1", stopWatch.Elapsed));
        var task2 = Task.Delay(3000).ContinueWith(_ => Completed("Task2", stopWatch.Elapsed));
        var task3 = Task.Delay(1000).ContinueWith(_ => Completed("Task3", stopWatch.Elapsed));
        await Task.WhenAny(task1, task2, task3);
        Console.WriteLine("WorkFlow:" + DateTime.Now);
        stopWatch.Stop();
    }

    static void Completed(string name, TimeSpan time)
    {
        Console.WriteLine($"{name}: {time}");
    }
}

