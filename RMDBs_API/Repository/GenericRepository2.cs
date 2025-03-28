using Microsoft.EntityFrameworkCore;
using RMDBs_API.Data;
using System.Linq.Expressions;

namespace RMDBs_API.Repositories
{
    public class GenericRepository2<T> : IGenericRepository2<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository2(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>> include = null
        )
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(
            int id,
            Func<IQueryable<T>, IQueryable<T>> include = null
        )
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();

        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await SaveChangesAsync();

            }
        }

        

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }
    }
}
