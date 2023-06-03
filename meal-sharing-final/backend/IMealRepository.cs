namespace HackYourFuture.Week7;
public interface IMealRepository
{
    Task<Meal[]> Search(string title);
    Task<Meal> PostMeal(Meal meal);
    Task<bool> PatchMeal(int id, Meal meal);
    Task<int> UpdatedMealById(Meal meal, int id);
}