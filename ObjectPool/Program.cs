using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using ObjectPool;
using System.Text;

//ObjectPoolSample.Test();

//Console.WriteLine("Press the enter key to exit.");



var sc = new ServiceCollection();
sc.TryAddSingleton<ObjectPoolProvider,DefaultObjectPoolProvider>();

sc.TryAddSingleton<Microsoft.Extensions.ObjectPool.ObjectPool<StringBuilder>>(serviceProvider =>
{
    var provider = serviceProvider.GetRequiredService<ObjectPoolProvider>();
    var policy = new StringBuilderPooledObjectPolicy();
    return provider.Create(policy);
});

var provider = sc.BuildServiceProvider();

var builderPool= provider.GetRequiredService<Microsoft.Extensions.ObjectPool.ObjectPool<StringBuilder>>();

var stringBuilder = builderPool.Get();

stringBuilder.Append("Hi ");

Console.WriteLine(stringBuilder.ToString());

builderPool.Return(stringBuilder);

Console.WriteLine(stringBuilder==null);

stringBuilder = builderPool.Get();

stringBuilder.Append("hhhh");
Console.Write(stringBuilder.ToString());

builderPool.Return(stringBuilder);

Console.ReadLine();
