using System.Buffers;
using System.Runtime.InteropServices.ComTypes;
using System.Security.AccessControl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using ObjectPool;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

// 1.0 FoobarList
// var objectPool = new ServiceCollection().AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>()
//     .BuildServiceProvider()
//     .GetRequiredService<ObjectPoolProvider>()
//     .Create(new FoobarListPolicy(1024,1024*1024));
// string json;
// var list = objectPool.Get();
//
// try
// {
//     list.AddRange(Enumerable.Range(1, 1000).Select(it => new Foobar(it, it)));
//     json = JsonSerializer.Serialize(list);
// }
// finally
// {
//     objectPool.Return(list);
// }

// 2.0 StringBuilderPool
// var objPool = new ServiceCollection().AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>()
//     .BuildServiceProvider()
//     .GetRequiredService<ObjectPoolProvider>()
//     .CreateStringBuilderPool(1024, 1024 * 1024);
// var builder = objPool.Get();
//
// try
// {
//     for (int index = 0; index < 100; index++)
//     {
//         builder.Append(index);
//     }
//
//     Console.WriteLine(builder);
// }
// finally
// {
//     objPool.Return(builder);
// }

// 3.0 ArrayPool<T>
// using var fs = new FileStream("test.txt", FileMode.Open);
// var length = (int)fs.Length;
// var bytes = ArrayPool<byte>.Shared.Rent(length);
// try
// {
//    await fs.ReadAsync(bytes, 0, length);
//    Console.WriteLine(Encoding.Default.GetString(bytes));
// }
// finally
// {
//     ArrayPool<byte>.Shared.Return(bytes);
// }

//4.0 MemoryPool<T>
using var fs = new FileStream("test.txt", FileMode.Open);
var length = (int)fs.Length;
var memoryOwner = MemoryPool<byte>.Shared.Rent(length);
await fs.ReadAsync(memoryOwner.Memory);
Console.WriteLine(Encoding.Default.GetString(memoryOwner.Memory.Span.Slice(0,length)));


//ObjectPoolSample.Test();

//Console.WriteLine("Press the enter key to exit.");
// 直接使用ObjectPool 创建对象
//var objPool = Microsoft.Extensions.ObjectPool.ObjectPool.Create<FoobarService>();

//使用依赖注入的方式 并传入 PooledObjectPolicy 来创建对象
// var objectPool = new ServiceCollection().AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>()
//     .BuildServiceProvider();
// var servicePool = objectPool.GetService<ObjectPoolProvider>()?.Create<FoobarService>(new FoobarServicePolicy());
//
// var poolSize = Environment.ProcessorCount * 2;
// while (true)
// {
//     //Console.Write("Used services: ");
//    // await Task.WhenAll(Enumerable.Range(1, poolSize+1).Select(_ => ExecuteAsync()));
//    await ExecuteAsync();
//     Console.WriteLine($"Last service: {FoobarService._latestId}");
//     // Console.Write("\n");
// }

// async Task ExecuteAsync()
// {
//     var service = servicePool?.Get();
//     
//     try
//     {
//         //if (service != null) Console.Write($"{service.Id};");
//         await Task.Delay(1000);
//     }
//     finally
//     {
//         servicePool.Return(service);
//     }
// }

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
