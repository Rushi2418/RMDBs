using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDBs_API.Data.Repositories
{
    public interface IGenericRepository3<T> where T : class
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
    }
}
