namespace HackYourFuture.Week7;
public interface IMealRepository
{
    Task<Meal[]> Search(string title);
    Task<Meal> PostMeal(Meal meal);
    Task<string> PatchMeal(int id, Meal meal);

}