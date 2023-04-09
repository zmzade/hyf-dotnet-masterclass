namespace HackYourFuture.Week7;
public interface IReservationsRepository
{
    Task<IEnumerable<Reservation>> GetAll();
}