using CinemaApp.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Repository
{
    public class Repository<TType, TId> : IRepository<TType, TId>
    where TType : class
    {
        private readonly CinemaDbContext dBcontext;
        private readonly DbSet<TType> dbSet;

        public Repository(CinemaDbContext dbContext)
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
        public IEnumerable<TType> GetAllAttached()
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

        public bool Delete(TId id)
        {
            TType entity = this.GetById(id);

            if (entity == null)
            {
                return false;
            }

            this.dbSet.Remove(entity);
            this.dBcontext.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteAsync(TId id)
        {
            TType entity = await this.GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

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
