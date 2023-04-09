namespace HackYourFuture.Week7;
using System.Text.Json.Serialization;
using Dapper;
using MySql.Data.MySqlClient;

public class MealRepository : IMealRepository
{
    private string connectionString;

    public MealRepository(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<Meal[]> Search(string? title)
    {
        var sql = $"SELECT * FROM heroku_490105da5b3e29e.meal";

        if (!string.IsNullOrEmpty(title))
        {
            sql += " WHERE title like @title";
        }

        using var connection = new MySqlConnection(connectionString);
        var meals = await connection.QueryAsync<Meal>(sql, new { title = "%" + title + "%" });
        if (meals.ToArray().Length == 0)
        {
            System.Console.WriteLine("Sorry! This meal is not available!");
        }
        return meals.ToArray();
    }

    public async Task<Meal> PostMeal(Meal meal)
    {
        try
        {
            var sql = @"INSERT INTO Meal (title, description, location,`when`, max_reservations, price, `created_date`) 
        VALUES (@title, @description, @location, @when, @maxReservations, @price, @createdDate)";
            using var connection = new MySqlConnection(connectionString);
            var affectedRow = await connection.ExecuteAsync(sql, meal);
            return meal;
        }
        catch (System.Exception)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task<string> PatchMeal(int id, Meal meal)
    {
        if (id < 0)
        {
            return ("Id can not be negative!");
        }
        var sql = @$"UPDATE meal SET title=@title WHERE id ={id}";
        using var connection = new MySqlConnection(connectionString);
        var updatedRow = await connection.ExecuteAsync(sql, meal);
        if (updatedRow == 0)
        {
            return ("Id not found!");
        }

        return $"meal's title changed to {meal.Title} at this Id:{meal.Id}";
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

