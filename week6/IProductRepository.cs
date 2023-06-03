namespace HackYourFuture.Week6;

public interface IProductRepository
{
    Task<Product> Get(int id);
    Task<Product> Create(Product product);
}