var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//homework week #1
//1. string manipulation:reverse
app.MapGet("/", () =>
{
    string input = "world";
    char[] chars = new char[input.Length];
    for (int i = 0; i < input.Length; i++)
    {
        chars[i] = input[(input.Length - 1) - i];
    }
    // var chars = input.ToCharArray().Reverse();
    return string.Join("", chars);
});

//2. Math Array
app.MapGet("/math", () =>
{
    int[] arr = new[] { 271, -3, 1, 14, -100, 13, 2, 1, -8, -59, -1852, 41, 5 };
    var mathArray = new int[2];
    var sum = 0;
    var product = 1;
    foreach (var number in arr)
    {
        if (number < 0) sum += number;
        if (number > 1) product *= number;
    }
    mathArray[0] = sum;
    mathArray[1] = product;
    return mathArray;
});



app.Run();
