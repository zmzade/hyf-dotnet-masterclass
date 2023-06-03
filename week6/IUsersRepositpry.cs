namespace HackYourFuture.Week6;

public interface IUserRepository
{
    Task<IEnumerable<User>> Get();
    Task<User> Create(User user);
    Task<int> Update(int id, User user);
    Task<int> Delete(int id);
}