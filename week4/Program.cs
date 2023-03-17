using System.Net.Http.Json;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapPost("/users", async (User user) =>
{
    var response = await new HttpClient().PostAsJsonAsync($"https://dummyjson.com/users/add", user);
    if (!response.IsSuccessStatusCode)
        return Results.BadRequest("Something went wrong!");
    var postedUser = await response.Content.ReadFromJsonAsync<User>();
    return Results.Ok(postedUser);
});
app.MapPost("/products", async () =>
{
    var response = await new HttpClient().PostAsJsonAsync($"https://dummyjson.com/products/add", new Product(10, "Laptop", 1000));
    if (!response.IsSuccessStatusCode)
        return Results.BadRequest("Something went wrong!");
    var postedProduct = await response.Content.ReadFromJsonAsync<Product>();
    return Results.Ok(postedProduct);
});

app.MapPost("/postget", async (PostRequest request) =>
{
    if (request.Endpoint == "users")
        return Results.Ok(await GetItemsAsync<User>(request));
    if (request.Endpoint == "products")
        return Results.Ok(await GetItemsAsync<Product>(request));
    return Results.BadRequest("Endpoint is not supported!");
});
async Task<List<T>> GetItemsAsync<T>(PostRequest request)
{
    var listOfTaskItems = new List<Task<T>>();

    foreach (var id in request.Ids)
    {
        var retrievedItem = GetItemAsync<T>(request.Endpoint, id);
        listOfTaskItems.Add(retrievedItem);
    }

    return (await Task.WhenAll(listOfTaskItems)).ToList();
}

async Task<T> GetItemAsync<T>(string endpoint, int id)
{
    var response = await new HttpClient().GetAsync($"https://dummyjson.com/{endpoint}/{id}");

    var item = await response.Content.ReadFromJsonAsync<T>();
    return (item);
}


app.MapGet("/get/{id}", async (int id) =>
   {
       var response = await new HttpClient().GetAsync($"https://dummyjson.com/users/{id}");
       if (!response.IsSuccessStatusCode)
           return Results.Ok("Something went wrong!");
       var user = await response.Content.ReadFromJsonAsync<User>();
       return Results.Ok(user);

   });


app.MapPut("/put/{id}", async (int id) =>
{
    var response = await new HttpClient().PutAsJsonAsync($"https://reqres.in/api/products/{id}", new Product(id, "Laptop", 1000));
    if (!response.IsSuccessStatusCode)
        return Results.Ok("Something went wrong!");
    var updatedProduct = await response.Content.ReadFromJsonAsync<Product>();
    return Results.Ok(updatedProduct);
});


app.MapDelete("/delete/{id}", async (int id) =>
{
    var response = await new HttpClient().DeleteAsync($"https://dummyjson.com/users/{id}");
    if (!response.IsSuccessStatusCode)
        return Results.BadRequest("something went wrong");
    var updatedUser = await response.Content.ReadFromJsonAsync<User>();
    return Results.Ok(updatedUser);

});

app.Run();
record PostRequest(string Endpoint, List<int> Ids);
record User(int Id, string FirstName, string LastName, int Age);
record Product(int Id, string Title, int Price);
