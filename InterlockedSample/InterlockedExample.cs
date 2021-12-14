using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterlockedSample
{
    internal class InterlockedExample
    {
        private static int usingResource = 0;
        private const int numThreadIterations = 5;
        private const int numThreads = 10;
        public static void InterlockedExchange()
        {
            Thread myThread;
            Random random = new Random();
            for (int i = 0; i < numThreads; i++)
            {
                myThread = new Thread(new ThreadStart(MyThreadProc));
                myThread.Name = String.Format("Thread{0}", i + 1);
                //Wait a random amount of time before starting next thread.
                Thread.Sleep(random.Next(0, 1000));
                myThread.Start();
            }
        }

        private static void MyThreadProc()
        {
            for (int i = 0; i < numThreadIterations; i++)
            {
                UseResource();
                Thread.Sleep(1000);
            }
        }

        static bool UseResource()
        {
            // 如果usingResource 交换之前为0 ,就可以获取锁执行代码
            if (0 == Interlocked.Exchange(ref usingResource, 1))
            {
                Console.WriteLine("{0} acquired the lock", Thread.CurrentThread.Name);

                Thread.Sleep(500);
                Console.WriteLine("{0} exiting lock", Thread.CurrentThread.Name);
                Interlocked.Exchange(ref usingResource, 0);
                return true;
            }
            else
            {
                Console.WriteLine("{0} was denied the lock",Thread.CurrentThread.Name); 
                return false;
            }
        }
    }
}
