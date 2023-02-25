var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//homework week #1
app.MapGet("/", () =>
{
    //1. string manipulation:reverse
    string input = "world";
    char[] chars = new char[input.Length];
    for (int i = 0; i < input.Length; i++)
    {
        chars[i] = input[(input.Length - 1) - i];
    }
    // var chars = input.ToCharArray().Reverse();
    return string.Join("", chars);



});

app.Run();
