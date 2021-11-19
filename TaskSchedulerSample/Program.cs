
using TaskSchedulerSample;

for (int i = 0; i < 800; i++)
{
   await Task.Factory.StartNew(async () =>
    {
        await TaskFactorySample.DoSomething("task" + i);
    },TaskCreationOptions.LongRunning);   //使用LongRunning 可以快速开启新的线程

}

Console.ReadKey();
