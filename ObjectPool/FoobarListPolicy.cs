using Microsoft.Extensions.ObjectPool;

namespace ObjectPool;

public class FoobarListPolicy:PooledObjectPolicy<List<Foobar>>
{
    private readonly int _initCapacity;
    private readonly int _maxCapacity;

    public FoobarListPolicy(int initCapacity, int maxCapacity)
    {
        _initCapacity = initCapacity;
        _maxCapacity = maxCapacity;
    }
    
    public override List<Foobar> Create()
    {
        return new List<Foobar>(_initCapacity);
    }

    public override bool Return(List<Foobar> obj)
    {
        if (obj.Capacity > _maxCapacity) return false;
        obj.Clear();
        return true;

    }
}

public class Foobar
{
    public  int Foo { get; }
    public  int Bar { get; }

    public Foobar(int foo, int bar)
    {
        Foo = foo;
        Bar = bar;
    }
}