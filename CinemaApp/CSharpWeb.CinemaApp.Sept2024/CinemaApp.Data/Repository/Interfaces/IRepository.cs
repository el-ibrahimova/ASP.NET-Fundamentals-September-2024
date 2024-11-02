using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CinemaApp.Data.Repository.Interfaces
{
    public interface IRepository<TType, TId>
    {
        TType GetById(TId id);
        Task<TType> GetByIdAsync(TId id);
        Task<TType> GetByIdAsync(params TId[] id);

        TType FirstOrDefault(Func<TType, bool> predicate);

        Task<TType> FirstOrDefaultAsync(Expression<Func<TType, bool>> predicate);
        
        IEnumerable<TType> GetAll();
        Task<IEnumerable<TType>> GetAllAsync();

        // we stay attached to the base
        IQueryable<TType> GetAllAttached();

        void Add(TType item);
        Task AddAsync(TType item);



        void AddRange(TType items);
        Task AddRangeAsync(TType[] items);

        bool Delete(TType entity);
        Task<bool> DeleteAsync(TType entity);

        bool Update(TType item);
        Task<bool> UpdateAsync(TType item);
    }
}
