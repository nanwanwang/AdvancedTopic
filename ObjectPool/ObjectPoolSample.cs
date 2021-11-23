using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPool
{
    internal class ObjectPoolSample
    {
         public static void Test()
        {
            using var cts=new CancellationTokenSource();

            Task.Run(() => 
            {
               if (char.ToUpperInvariant(Console.ReadKey().KeyChar) == 'C')
                {
                    cts.Cancel();
                }
            });

            var pool = new ObjectPool<ExampleObject>(()=>new ExampleObject());

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Parallel.For(0, 1000, (i, loopState) => 
            {
                var example = pool.Get();
                try
                {
                    Console.CursorLeft = 0;
                    //Console.WriteLine($"{example.GetValue(i):####.####}");
                }
                finally
                {
                    pool.Return(example);
                }
                if (cts.Token.IsCancellationRequested)
                {
                    loopState.Stop();
                }
            });
            Console.WriteLine(sw.ElapsedMilliseconds); 



            Parallel.For(0, 1000, (i, loopState) =>
            {
                var example = new ExampleObject();
                try
                {
                    Console.CursorLeft = 0;
                    //Console.WriteLine($"{example.GetValue(i):####.####}");
                }
                finally
                {
                    pool.Return(example);
                }
                if (cts.Token.IsCancellationRequested)
                {
                    loopState.Stop();
                }
            });
            Console.WriteLine(sw.ElapsedMilliseconds);



        }
    }

    internal class ObjectPool<T>
    {
        private readonly ConcurrentBag<T> _objects;
        private Func<T> _objectGenerator;

        public ObjectPool(Func<T> objectGenerator)
        {
            _objectGenerator = objectGenerator?? throw new ArgumentNullException(nameof(objectGenerator));
            _objects = new ConcurrentBag<T>();
        }

        public T Get() => _objects.TryTake(out T item) ? item : _objectGenerator();

        public void Return(T item)=> _objects.Add(item);


        public override string ToString()
        {
            return "对象数量"+_objects.Count.ToString();
        }
    }

    internal class ExampleObject
    {
        public int[] Nums { get; set; }

        public ExampleObject()
        {
            Nums = new int[1_000_000];
            var rand = new Random();
            for (int i = 0; i < Nums.Length; i++)
            {
                Nums[i] = rand.Next();
            }
        }

        public double GetValue(long i) => Math.Sqrt(Nums[i]);
    }
}
