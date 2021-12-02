using InterlockedSample;

int a = 1;
int b = 0;
//int res=Interlocked.Exchange(ref a, b);
//Console.WriteLine($"res={res}");
//Console.WriteLine($"a={a}");
//Console.WriteLine($"b={b}");
//int c = 0;
//int res2=Interlocked.CompareExchange(ref  c, a, b);
//Console.WriteLine($"res={res2}");
//Console.WriteLine($"a={a}");
//Console.WriteLine($"b={b}");
//Console.WriteLine($"c={c}");
//InterlockedExample.InterlockedExchange();


//var res3 = Interlocked.Increment(ref a);
//Console.WriteLine(res3);



Thread thread1 = new Thread(new ThreadStart(akshay.ThreadMethod));
Thread thread2 = new Thread(new ThreadStart(akshay.TrheadMethod2));
thread1.Start();
thread1.Join();
thread2.Start();
thread2.Join();

Console.WriteLine(akshay.CountClass.UnsafeInstanceCount);
Console.WriteLine(akshay.CountClass.SafeInstanceCount);
Console.Read();

Console.ReadKey();