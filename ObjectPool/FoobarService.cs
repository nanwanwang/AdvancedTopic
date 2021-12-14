using Microsoft.Extensions.ObjectPool;

namespace ObjectPool;

public class FoobarService:IDisposable
{
    internal static int _latestId;
    
    public  int Id { get; }

    private FoobarService() => Id = Interlocked.Increment(ref _latestId);

    public static FoobarService Create() => new FoobarService();

    public void Dispose()
    {
        Console.WriteLine($"service disposed {this.Id}");
    }
}

public class FoobarServicePolicy : PooledObjectPolicy<FoobarService>
{
    public override FoobarService Create()
    {
       return  FoobarService.Create();
    }

    public override bool Return(FoobarService obj)
    {
        return true;
    }
}