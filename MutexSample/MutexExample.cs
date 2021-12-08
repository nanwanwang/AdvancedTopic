using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutexSample
{
    internal class MutexExample
    {
        static Mutex _m;
        static Mutex _lock = new Mutex ();
        static object obj = new object ();
        static ManualResetEvent ManualReset=new ManualResetEvent (false);
        static AutoResetEvent AutoResetEvent=new AutoResetEvent (false);
        static int count = 0;

        public static bool IsSingleInstance()
        {
            try
            {
             
                Mutex.OpenExisting("demon");
            }
            catch 
            {
                _m = new Mutex(true, "demon");
                return true;
                
            }
            return false;
        }

        public static  void MutexLock()
        {
            var list = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                Thread thread = new Thread(() => 
                {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                    //count++;
                    //lock
                    //lock (obj)
                    //{
                    //    count++;
                    //}
                    // Interlock
                    //Interlocked.Increment(ref count);
                    //metux
                    //_lock.WaitOne();
                    //count++;
                    //_lock.ReleaseMutex();

                    //manualresetevent
                    //ManualReset.Set();
                    //count++;
                    //ManualReset.Reset ();

                    //autoresetevent
                    AutoResetEvent.Set();
                    count++;
                    AutoResetEvent.Reset();
                });
                thread.IsBackground = true;
                thread.Start();
                thread.Join();

               //var task = new Task(()=> {
               //    lock (obj)
               //    {
               //        count++;
               //    }

                   //
                  
                   //ManualReset.WaitOne();
                   //count++;
                   //ManualReset.Set();

               //});
               // task.Start();
               // list.Add(task);
            }
              //Task.WaitAll(list.ToArray());
            //Task.WhenAll(list).Wait();
       //     Task.WaitAll(list.ToArray());
            Console.WriteLine("count :"+count);
        }
    }
}
