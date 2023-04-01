using Dapper;
using MySql.Data.MySqlClient;
namespace HackYourFuture.Week6;


public record User(int Id, string Fullname, string Address);

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers();
    Task<User> PostUser(User user);
    Task<int> UpdateUser(int id, User user);
    Task<int> DeleteUser(int id);
}


public class UserRepository : IUserRepository
{
    private string connectionString;

    public UserRepository(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        using var connection = new MySqlConnection(connectionString);
        var users = await connection.QueryAsync<User>("SELECT * FROM dapper.users");
        return users;
    }

    public async Task<User> PostUser(User user)
    {
                
        var sql = @"INSERT INTO users (fullname, address) VALUES (@fullname, @address)";
        using var connection = new MySqlConnection(connectionString);
        var affectedRow = await connection.ExecuteAsync(sql, user);
        return user;
    }

    public async Task<int> UpdateUser(int id, User user)
    {
        var sql = @$"UPDATE users SET fullname=@fullname, address=@address WHERE id ={id}";
        using var connection = new MySqlConnection(connectionString);
         var affectedRow = await connection.ExecuteAsync(sql, user);
         return affectedRow;
    }

    public async Task<int> DeleteUser(int id)
    {
        var sql = $"DELETE FROM users WHERE id=@id";
         using var connection = new MySqlConnection(connectionString);
         var affectedRow = await connection.ExecuteAsync(sql, new{id=id});
         return affectedRow;
    }
}
