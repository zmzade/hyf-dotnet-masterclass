using Dapper;
using MySql.Data.MySqlClient;
namespace HackYourFuture.Week6;

public class ProductRepository : IProductRepository
{
    private string _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        this._connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<Product> Get(int id)
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

    public async Task<Product> Create(Product product)
    {
        var sql = "INSERT INTO products (name, price, description) VALUES (@name, @price, @description)";
        using var connection = new MySqlConnection(connectionString);
        var affectedRow = await connection.ExecuteAsync(sql, product);
        return product;

    }
}

public record Product(int Id, string Name, decimal? Price, string description = "");
