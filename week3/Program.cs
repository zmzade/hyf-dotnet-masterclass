var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/interface", () =>
{
    var cat = new Cat();
    var dog = new Dog();
    var cow = new Cow();
    var animal = new Animal();
    return animal.MakeSound(cat);
    return "hi";
});

app.Run();

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