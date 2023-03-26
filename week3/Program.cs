var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", (decimal input, decimal rate) =>
{
    var exchangeRate = new ExchangeRate("DKK", "EUR");
    var amount = input;
    exchangeRate.Rate = rate;
    exchangeRate.Calculate(input);
    return $"{amount} {exchangeRate.FromCurrency} is {exchangeRate.Calculate(amount)} {exchangeRate.ToCurrency}";
});

app.MapGet("/temperature", (decimal input) =>
{
    var temperature = new Temperature(input);
    return $"{temperature.Celsius} Celsius is {temperature.Fahrenheit} Fahrenheit and {temperature.Kelvin} Kelvin";
});

app.MapGet("/interface", () =>
{
    var cat = new Cat();
    var dog = new Dog();
    var cow = new Cow();

    return MakeSound(cat);

    string MakeSound(IAnimal animal)
    {
        return $"{animal.Name} says {animal.Sound}";
    }
});

app.MapGet("/account", () =>
{
    var account = new Account(200);
    account.Withdraw(100);
    account.Deposit(50);
    return account.Balance;
});

app.Run();

class Account
{
    private int _balance;
    public int Balance
    {
        get
        {
            return _balance;
        }
    }
    public Account(int amount)
    {
        _balance = amount;
    }


    public int Withdraw(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Withdraw should be positive");
        if (Balance - amount < 0)
            throw new InvalidOperationException("we should not be able to withdraw more than we have in the balance");

        return _balance -= amount;
    }

    public int Deposit(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Deposit should be positive");
        return _balance += amount;
    }
}

public interface IAnimal
{
    string Name { get; }
    string Sound { get; }
}

class Cat : IAnimal
{
    public string Name { get; set; } = "cat";
    public string Sound { get; set; } = "meow meow";
}
class Dog : IAnimal
{
    public string Name { get; set; } = "dog";
    public string Sound { get; set; } = "woof woof";
}
class Cow : IAnimal
{
    public string Name { get; set; } = "cow";
    public string Sound { get; set; } = "maa maa";
}
