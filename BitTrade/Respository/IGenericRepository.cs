namespace BitTrade.Respository
{
    public interface IGenericRepository<T>
    {
        Task<T> Create(T entity);
        Task Delete(T entity);
        Task<bool> Exists(string id);
        IQueryable<T> GetAllAsQueryable();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(string id);
        Task Save();
        Task Update(T entity);
    }
}