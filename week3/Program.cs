var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/interface", () =>
{
    var cat = new Cat();
    var dog = new Dog();
    var cow = new Cow();
    var animal = new Animal();
    return animal.MakeSound(cat);
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
    public int Balance
    {
        get
        {
            int balance = 0;
            foreach (var item in transactions)
            {
                balance += item.Amount;
            }
            return balance;
        }
    }
    public Account(int amount)
    {
        Deposit(amount);
    }

    public List<Transaction> transactions = new List<Transaction>();
    public void Withdraw(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Withdraw should be positive");
        if (Balance - amount < 0)
            throw new InvalidOperationException("we should not be able to withdraw more than we have in the balance");
        var transaction = new Transaction(-amount);
        transactions.Add(transaction);
    }

    public void Deposit(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Deposit should be positive");
        var transaction = new Transaction(amount);
        transactions.Add(transaction);
    }
}

class Transaction
{
    public int Amount { get; }
    public Transaction(int amount)
    {
        Amount = amount;
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

class Animal
{
    public string MakeSound(IAnimal animal)
    {
        return $"{animal.Name} sags {animal.Sound}";
    }
}