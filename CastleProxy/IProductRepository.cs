namespace CastleProxy;

public interface IProductRepository
{ 
    void Update(Product product);
    Task<int> UpdateAsync(Product product);

}