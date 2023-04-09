using System.Text.Json.Serialization;
using Dapper;
using MySql.Data.MySqlClient;
namespace HackYourFuture.Week7;

public class ReservationsRepository : IReservationsRepository
{
    private string connectionString;

    public ReservationsRepository(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<IEnumerable<Reservation>> GetAll()
    {
        var sql = "SELECT * FROM heroku_490105da5b3e29e.reservation";
        using var connection = new MySqlConnection(connectionString);
        var reservations = await connection.QueryAsync<Reservation>(sql);
        return reservations.ToArray();
    }
}

public class Reservation
{
    public int Id { get; set; }
    [JsonPropertyName("number_of_guests")]
    public int NumberOfGuests { get; set; }
    [JsonPropertyName("meal_id")]
    public int MealId { get; set; }
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }
    [JsonPropertyName("contact_phonenumber")]
    public string ContactPhonenumber { get; set; }
    [JsonPropertyName("contact_name")]
    public string ContactName { get; set; }
    [JsonPropertyName("contact_email")]
    public string ContactEmail { get; set; }
}