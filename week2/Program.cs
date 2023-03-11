var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//word frequency count
app.MapGet("/", (string input) =>
{
    var words = input.Split(" ");

    var wordsList = new Dictionary<string, int>();
    foreach (var word in words)
    {
        if (!wordsList.ContainsKey(word))
        {
            wordsList.Add(word.ToLower(), 1);
        }
        else
        {
            wordsList[word]++;
        }
    }

    return wordsList;

});

app.Run();
