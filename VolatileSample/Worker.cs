using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolatileSample
{
    internal class Worker
    {
        private volatile  bool _shouldStop;
        public void DoWork()
        {
            while (!_shouldStop)
            {
                Console.WriteLine("Worker thread:working...");
            }
            Console.WriteLine("Worker thread: terminating gracefully.");
        }

        public void RequestStop()
        {
            _shouldStop = true;
        }
    }
}
