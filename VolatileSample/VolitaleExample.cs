using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolatileSample
{
    internal class VolitaleExample
    {
        volatile int m_flag = 0;
        int m_value =1;
        public   void TestVolitale()
        {
            Thread2();
            Thread1();
       
        }

        public  void Thread1()
        {
            Task.Run(() => 
            {
                m_value++;
                m_flag = 1;
                
            });
        }

        public void Thread2()
        {
            Task.Run(() =>
            {
               if(m_flag == 1)
                {
                    Console.WriteLine(m_value);
                }
                else
                {
                    Console.WriteLine("m_flag !=1");
                }
              

            });
        }

        public void WorkerTest()
        {
            Worker workerObject = new Worker();
            Thread workerThread= new Thread(workerObject.DoWork);

            workerThread.Start();
            Console.WriteLine("Main thread: starting woker thread ...");

            while (!workerThread.IsAlive) ;
            Thread.Sleep(1);

            workerObject.RequestStop();

            workerThread.Join();

            Console.WriteLine("Main thread: worker thread has terminated.");
        }
    }
}
