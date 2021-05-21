using Boarsenger.API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Boarsenger.API.DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IDbEntity
    {
        Task CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> condition);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition);

        Task<TResult> GetAsync<TResult>(
            Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TResult>> selector);

        Task<IEnumerable<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> condition,
            int pageNumber, int pageSize);

        Task<IEnumerable<TResult>> GetPageAsync<TResult>(Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TResult>> selector, int pageNumber, int pageSize);

        Task DeleteAsync(Expression<Func<TEntity, bool>> condition);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition);

        Task<bool> ContainsAsync(TEntity entity);

        Task SaveAsync();
    }
}
