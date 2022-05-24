using MarketingCoinBase.IRepositories;
using MarketingCoinBase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MarketingCoinBase.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CoinBaseDB _context;
        public GenericRepository(CoinBaseDB context)
        {
            _context = context; 
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveCompletedAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await SaveCompletedAsync();
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return expression != null ? await _context.Set<T>().AsQueryable().Where(expression).AsNoTracking().ToListAsync() :
              await _context.Set<T>().AsQueryable().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void RemoveAsync(T entity)
        {
             _context.Set<T>().Remove(entity);
            
        }

        public void RemoveRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<bool> SaveCompletedAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
