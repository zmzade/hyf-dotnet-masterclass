using HackYourFuture.Week7;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IReservationsRepository, ReservationsRepository>();
builder.Services.AddScoped<IReviewsRepository, ReviewsRepository>();
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.MapGet("/api/meals", async (IMealRepository mealRepository, string title) =>
{
    return await mealRepository.Search(title);
});

app.MapPost("/api/meals", async (IMealRepository mealRepository, Meal meal) =>
{
    Meal newMeal = await mealRepository.PostMeal(meal);
    return newMeal != null ? Results.Ok(newMeal) : Results.BadRequest("No new meal added");
});

app.MapPut("/api/meals/{id}", async (IMealRepository mealRepository, Meal meal, int id) =>
{
    var updatedMeal = await mealRepository.UpdatedMealById(meal, id);
    return updatedMeal != 0 ? Results.Ok(meal) : Results.BadRequest("Update fails!");
});

app.MapPatch("/api/meals/{id}", async (IMealRepository mealRepository, int id, Meal meal) =>
{
    return await mealRepository.PatchMeal(id, meal);
});

app.MapGet("/api/reservations", async (IReservationsRepository reservationsRepository) =>
{
    return await reservationsRepository.GetAll();
});

app.MapGet("/api/reviews", async (IReviewsRepository reviewsRepository) =>
{
    return await reviewsRepository.GetAll();
});

app.Run();
