using Dapper;
using MySql.Data.MySqlClient;

namespace HackYourFuture.Week6;


public interface IProductRepository
{
    Task<Product> GetProduct(int id);
    Task<Product> PostProduct(Product product);
}

public class ProductRepository : IProductRepository
{
    private string connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<Product> GetProduct(int id)
    {
    
        var sql = $"SELECT * FROM dapper.products WHERE id={id}";
        using var connection = new MySqlConnection(connectionString);
        try
        {
        var product = await connection.QuerySingleAsync<Product>(sql);
        return product;    
        }
        catch (System.Exception)
        {
            throw new Exception("Id not found");
        }
    }

    public async Task<Product> PostProduct(Product product)
    {
      var sql = "INSERT INTO products (name, price, description) VALUES (@name, @price, @description)";
      using var connection = new MySqlConnection(connectionString);
      var affectedRow = await connection.ExecuteAsync(sql, product);
      return product;

    }
}

public record Product(int Id, string Name, decimal? Price, string description="");
