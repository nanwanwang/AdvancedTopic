using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerSample;

public class TaskFactorySample
{

    public static async Task DoSomething(string taskName)
    {
        var bytes = new byte[1024*1024];
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{taskName} 的线程id=>{Thread.CurrentThread.ManagedThreadId}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{taskName} 开始:{DateTime.Now:hh:mm:ss}");
        while (true)
        {
            bytes[0] = 1;
            Console.WriteLine($"{taskName}:{1}");
            await Task.Delay(1000);
        }
    }
}

