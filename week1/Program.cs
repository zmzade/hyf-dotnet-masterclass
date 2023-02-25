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
//return an array with first element= sum of negatives and second= product of positives;
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

//3. String/Math
//Return the number (count) of vowels in the given string.
app.MapGet("/stringmath", () =>
{
    string input = "Intellectualization";
    var vowels = new char[5] { 'a', 'i', 'e', 'o', 'u' };
    var count = 0;
    foreach (var vowel in input.ToLower())
    {
        if (vowels.Contains(vowel)) count++;
    }
    return count;
});

//4. Create function Fibonacci that returns N'th element of Fibonacci sequence
//int n = 6 => Nth fibonacci number is 8
app.MapGet("/fib", () =>
{
    int n = 6;
    var fibList = new List<int>() { 1, 1 };

    for (var i = 2; i < n; i++)
    {
        fibList.Add(fibList[fibList.Count - 1] + fibList[fibList.Count - 2]);
    }
    return fibList[fibList.Count - 1];

});

app.Run();
