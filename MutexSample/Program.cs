using MutexSample;
using System.Diagnostics;
//if (!MutexExample.IsSingleInstance())
//{
//    Console.WriteLine("more than one instance");
//}else
//{
//    Console.WriteLine("one instance");
//}

Stopwatch sw = new Stopwatch();

sw.Start();
MutexExample.MutexLock(); 
Console.WriteLine(sw.ElapsedMilliseconds);
sw.Stop();
Console.ReadLine();
