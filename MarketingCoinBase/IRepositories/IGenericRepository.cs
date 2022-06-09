using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MarketingCoinBase.IRepositories
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<T> GetByIdAsync(long id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void RemoveAsync(T entity);
        void Update(T entity);  
        void UpdateRange(IEnumerable<T> entities);
        void RemoveRangeAsync(IEnumerable<T> entities);
        Task<T> FindSingleAsync(Expression<Func<T, bool>> expression);
        Task<bool> SaveCompletedAsync();
    }
}
