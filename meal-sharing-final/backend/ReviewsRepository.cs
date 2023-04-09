namespace HackYourFuture.Week7;
using System.Text.Json.Serialization;
using Dapper;
using MySql.Data.MySqlClient;

public class ReviewsRepository : IReviewsRepository
{
    private string connectionString;
    public ReviewsRepository(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<Review[]> GetAll()
    {
        var sql = "SELECT * FROM heroku_490105da5b3e29e.review";
        using var connection = new MySqlConnection(connectionString);
        var reviews = await connection.QueryAsync<Review>(sql);
        return reviews.ToArray();
    }
}


public class Review
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    [JsonPropertyName("meal_id")]
    public int MealId { get; set; }
    public string stars { get; set; }
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }
}