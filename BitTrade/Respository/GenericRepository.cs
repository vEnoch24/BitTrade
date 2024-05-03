using BitTrade.Data;
using Microsoft.EntityFrameworkCore;

namespace BitTrade.Respository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BitTradeDbContext _dbcontext;

        public GenericRepository(BitTradeDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<T> Create(T entity)
        {
            await _dbcontext.AddAsync(entity);
            return entity;
        }

        public async Task Delete(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
        }

        public async Task<bool> Exists(string id)
        {
            var entity = await GetById(id);
            return entity != null;
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return _dbcontext.Set<T>().AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task Save()
        {
            await _dbcontext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbcontext.Entry(entity).State = EntityState.Modified;
        }
    }
}
