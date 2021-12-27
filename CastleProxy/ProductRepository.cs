namespace CastleProxy;

public class ProductRepository:IProductRepository
{
    public virtual void Update(Product product)
    {
        Console.WriteLine("update data");
    }

    public virtual Task<int> UpdateAsync(Product product)
    {
        Console.WriteLine($"{nameof(UpdateAsync)} entry");

        return Task.Run(async () =>
        {
            await Task.Delay(1000);
            Console.WriteLine($"{nameof(UpdateAsync)} 更新操作已完成");
            return 1;
        });
    }
}