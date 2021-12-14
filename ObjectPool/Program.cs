using System.Runtime.InteropServices.ComTypes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using ObjectPool;
using System.Text;


//ObjectPoolSample.Test();

//Console.WriteLine("Press the enter key to exit.");
// 直接使用ObjectPool 创建对象
//var objPool = Microsoft.Extensions.ObjectPool.ObjectPool.Create<FoobarService>();

//使用依赖注入的方式 并传入 PooledObjectPolicy 来创建对象
var objectPool = new ServiceCollection().AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>()
    .BuildServiceProvider();
var servicePool = objectPool.GetService<ObjectPoolProvider>()?.Create<FoobarService>(new FoobarServicePolicy());

var poolSize = Environment.ProcessorCount * 2;
while (true)
{
    //Console.Write("Used services: ");
   // await Task.WhenAll(Enumerable.Range(1, poolSize+1).Select(_ => ExecuteAsync()));
   await ExecuteAsync();
    Console.WriteLine($"Last service: {FoobarService._latestId}");
    // Console.Write("\n");
}

async Task ExecuteAsync()
{
    var service = servicePool?.Get();
    
    try
    {
        //if (service != null) Console.Write($"{service.Id};");
        await Task.Delay(1000);
    }
    finally
    {
        servicePool.Return(service);
    }
}

// var sc = new ServiceCollection();
// sc.TryAddSingleton<ObjectPoolProvider,DefaultObjectPoolProvider>();
//
// sc.TryAddSingleton<Microsoft.Extensions.ObjectPool.ObjectPool<StringBuilder>>(serviceProvider =>
// {
//     var provider = serviceProvider.GetRequiredService<ObjectPoolProvider>();
//     var policy = new StringBuilderPooledObjectPolicy();
//     return provider.Create(policy);
// });
//
// var provider = sc.BuildServiceProvider();
//
// var builderPool= provider.GetRequiredService<Microsoft.Extensions.ObjectPool.ObjectPool<StringBuilder>>();
//
// var stringBuilder = builderPool.Get();
//
// stringBuilder.Append("Hi ");
//
// Console.WriteLine(stringBuilder.ToString());
//
// builderPool.Return(stringBuilder);
//
// Console.WriteLine(stringBuilder==null);
//
// stringBuilder = builderPool.Get();
//
// stringBuilder.Append("hhhh");
// Console.Write(stringBuilder.ToString());
//
// builderPool.Return(stringBuilder);

Console.ReadLine();
