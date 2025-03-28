using System.Linq.Expressions;

namespace RMDBs_API.Repositories
{
    public interface IGenericRepository2<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>> include = null
        );
        Task<T> GetByIdAsync(
            int id,
            Func<IQueryable<T>, IQueryable<T>> include = null
        );
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
