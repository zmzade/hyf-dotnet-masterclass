using Dapper;
using MySql.Data.MySqlClient;
namespace HackYourFuture.Week6;


// public record User(int Id, string Fullname, string Address);
public interface IRepository<T>
{
    Task<IEnumerable<T>> Get( string table);
    // Task<T> Post(T postItem);
    // Task<int> Put (int id, T putItem);
    // Task<int> Delete (int id);
}
public class Repository<T> : IRepository<T>
{
    private string connectionString;
    public Repository(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<IEnumerable<T>> Get(string table)
    {
        using var connection = new MySqlConnection(connectionString);
        var items = await connection.QueryAsync<T>($"SELECT * FROM dapper.{table}");
        return (IEnumerable<T>)items;
    }
}