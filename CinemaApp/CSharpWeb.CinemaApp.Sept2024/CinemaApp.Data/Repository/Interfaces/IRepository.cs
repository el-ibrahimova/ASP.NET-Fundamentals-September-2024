namespace CinemaApp.Data.Repository.Interfaces
{
    public interface IRepository<TType, TId>
    {
        TType GetById(TId id);
        TType GetByIdAsync(TId id);
    }
}
