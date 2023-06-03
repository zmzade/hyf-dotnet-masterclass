namespace HackYourFuture.Week7;

public interface IReviewsRepository
{
    Task<Review[]> GetAll();
}