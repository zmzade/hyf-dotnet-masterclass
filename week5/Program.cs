var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMealService, FileMealService>();
var app = builder.Build();

app.MapGet("/meals", (IMealService mealService) =>
{
    return mealService.ListMeals();
});

app.MapPost("/meal", (IMealService mealService, Meal meal) =>
{
    mealService.AddMeal(meal);
});

app.Run();

public interface IMealService
{
    List<Meal> ListMeals();
    void AddMeal(Meal meal);
}

public class FileMealService : IMealService
{
    public List<Meal> ListMeals()
    {
        if (!File.Exists("Meals.json"))
            File.WriteAllText("Meals.json", "[]");
        var json = File.ReadAllText("Meals.json");
        var meals = System.Text.Json.JsonSerializer.Deserialize<List<Meal>>(json);
        return meals;
    }

    public void AddMeal(Meal meal)
    {
        var meals = ListMeals();
        meals.Add(meal);
        var mealJson = System.Text.Json.JsonSerializer.Serialize(meals);
        File.WriteAllText("Meals.json", mealJson);
    }
}

public class Meal
{
    public string Headline { get; set; }
    public string ImageUrl { get; set; }
    public string Body { get; set; }
    public string Location { get; set; }
    public int Price { get; set; }
}
