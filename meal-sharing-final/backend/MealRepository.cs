namespace HackYourFuture.Week7;
using System.Text.Json.Serialization;
using Dapper;
using MySql.Data.MySqlClient;

public class MealRepository : IMealRepository
{
    private string _connectionString;

    public MealRepository(IConfiguration configuration)
    {
        this._connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<Meal[]> Search(string? title)
    {
        var sql = $"SELECT * FROM heroku_490105da5b3e29e.meal";

        if (!string.IsNullOrEmpty(title))
        {
            sql += " WHERE title like @title";
        }

        using var connection = new MySqlConnection(_connectionString);
        var meals = await connection.QueryAsync<Meal>(sql, new { title = "%" + title + "%" });
        if (meals.ToArray().Length == 0)
        {
            System.Console.WriteLine("Sorry! This meal is not available!");
        }
        return meals.ToArray();
    }

    public async Task<Meal> PostMeal(Meal meal)
    {
        var sql = @"INSERT INTO Meal (title, description, location,`when`, max_reservations, price, `created_date`) 
        VALUES (@title, @description, @location, @when, @maxReservations, @price, @createdDate)";
        using var connection = new MySqlConnection(_connectionString);
        var affectedRow = await connection.ExecuteAsync(sql, meal);
        return meal;
    }

    public async Task<int> UpdatedMealById(Meal meal, int id)
    {
        var sql = @$"UPDATE meal SET title=@title, description=@description, location=@location, 
        when=@when, max_reservations=@MmaxReservations, price=@price, 
        created_date@createdDate WHERE id= {id}";
        using var connection = new MySqlConnection(_connectionString);
        var row = await connection.ExecuteAsync(sql, meal);
        return row;
    }

    public async Task<bool> PatchMeal(int id, Meal meal)
    {
        var sql = @$"UPDATE meal SET title=@title WHERE id ={id}";
        using var connection = new MySqlConnection(_connectionString);
        var updatedRow = await connection.ExecuteAsync(sql, meal);
        return updatedRow != 0;
    }

}

public class Meal
{
    public int Id { get; set; }
    public string Title { get; set; }
    [JsonPropertyName("max_reservations")]
    public int MaxReservations { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public decimal Price { get; set; }
    public DateTime When { get; set; }
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }
}



