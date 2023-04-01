using HackYourFuture.Week6;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.MapGet("/users", async (IUserRepository userRepository) =>
{
    return await userRepository.GetUsers();
});

app.MapPost("/user", async(IUserRepository userRepository, User user)=>
{
    if(string.IsNullOrWhiteSpace(user.Fullname))
        throw new Exception("Fullname is required!");
        if (string.IsNullOrWhiteSpace(user.Address))
        throw new Exception("Address is required!");
    return await userRepository.PostUser(user);
});

app.MapPut("/updateUser/{id}", async(IUserRepository userRepository, int id, User user)=>
{
    return await userRepository.UpdateUser(id, user);
});

app.MapDelete("/deleteUser/{id}", async(int id, IUserRepository userRepository)=>
{
    return await userRepository.DeleteUser(id);
});

app.MapGet("/product/{id}", async(int id, IProductRepository productRepository)=>
{
    if (id < 0)
    throw new Exception("Id should be a positive number");

    return await productRepository.GetProduct(id);
    
});

app.MapPost("/product", async(IProductRepository productRepository, Product product)=>
{
    if(string.IsNullOrWhiteSpace(product.Name))
    throw new Exception("Product's name is required!");
    if (product.Price is null)
    throw new Exception("Product's price is required!");
    return await productRepository.PostProduct(product);
});


app.Run();

