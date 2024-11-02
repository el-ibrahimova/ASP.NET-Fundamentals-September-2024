using System.Linq.Expressions;

namespace CinemaApp.Data.Repository
{
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class BaseRepository<TType, TId> : IRepository<TType, TId>
    where TType : class
    {
        private readonly CinemaDbContext dBcontext;
        private readonly DbSet<TType> dbSet;

        public BaseRepository(CinemaDbContext dbContext)
        {
            this.dBcontext = dbContext;
            this.dbSet = this.dBcontext.Set<TType>();
        }

        // we set nullable to disabled => from CinemaApp.Data=>Properties
        public TType GetById(TId id)
        {
            TType entity = this.dbSet
                .Find(id);

            return entity;
        }

        public async Task<TType> GetByIdAsync(TId id)
        {
            TType entity = await this.dbSet
                               .FindAsync(id);
            return entity;
        }


        // Fix ... ASAP
        public async Task<TType> GetByIdAsync(params TId[] id)
        {
            TType entity = await this.dbSet
                .FindAsync(id[0], id[1]);
            return entity;
        }

        public TType FirstOrDefault(Func<TType, bool> predicate)
        {
            TType entity = this.dbSet
                .FirstOrDefault(predicate);

            return entity;
        }

        public async Task<TType> FirstOrDefaultAsync(Expression<Func<TType, bool>> predicate)
        {
            TType entity = await this.dbSet
                .FirstOrDefaultAsync(predicate);

            return entity;
        }

        // we disconnect from the base with .ToArray
        public IEnumerable<TType> GetAll()
        {
            return this.dbSet.ToArray();
        }

        public async Task<IEnumerable<TType>> GetAllAsync()
        {
            return await this.dbSet.ToArrayAsync();
        }

        // we stay attached to the base
        public IQueryable<TType> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }


        public void Add(TType item)
        {
            this.dbSet.Add(item);
            this.dBcontext.SaveChanges();
        }

        public async Task AddAsync(TType item)
        {
            await this.dbSet.AddAsync(item);
            await this.dBcontext.SaveChangesAsync();
        }

        public void AddRange(TType items)
        {
            this.dbSet.AddRange(items);
            this.dBcontext.SaveChanges();
        }

        public async Task AddRangeAsync(TType[] items)
        {
            await this.dbSet.AddRangeAsync(items);
            await this.dBcontext.SaveChangesAsync();
        }

        public bool Delete(TType entity)
        {
            this.dbSet.Remove(entity);
            this.dBcontext.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteAsync(TType entity)
        {
            this.dbSet.Remove(entity);
            await this.dBcontext.SaveChangesAsync();
            return true;
        }

        public bool Update(TType item)
        {
            try
            {
                this.dbSet.Attach(item);
                this.dBcontext.Entry(item).State = EntityState.Modified;
                this.dBcontext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TType item)
        {
            try
            {
                this.dbSet.Attach(item);
                this.dBcontext.Entry(item).State = EntityState.Modified;
                await this.dBcontext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
