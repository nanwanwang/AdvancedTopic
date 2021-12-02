using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterlockedSample
{
    public class akshay
    {
       public static void ThreadMethod()
        {
            for (int i = 0; i < 10; i++)
            {
               Task.Run(() => CountClass.Increase());
            }
        }

        public static void TrheadMethod2()
        {
            for (int i = 0; i < 10; i++)
            {
                Task.Run(() => CountClass.Decrease());
            }
        }

      public  class CountClass
        {
            public object lock1 = new object();
            public object lock2 = new object(); 
            static int unsafeInstanceCount = 0;
            static int safeInstanceCount = 0;
            static public int UnsafeInstanceCount
            {
                get { return unsafeInstanceCount; }
            }
            static public int SafeInstanceCount
            {
                get { return safeInstanceCount; }
            }

            public static void Increase()
            {
                unsafeInstanceCount++;
              Interlocked.Increment(ref safeInstanceCount);
            }

            public static void Decrease()
            {
                unsafeInstanceCount--;
                Interlocked.Decrement(ref safeInstanceCount);
            }
        }
    }
}
