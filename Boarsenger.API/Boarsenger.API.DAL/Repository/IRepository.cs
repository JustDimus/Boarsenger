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
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition);

        Task<TOut> GetAsync<TOut>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TOut>> selector);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, int take, int skip);

        Task<TOut> GetAsync<TOut>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TOut>> selector, int take, int skip);

        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> condition,
            IEnumerable<Expression<Func<TEntity, dynamic>>> includes);

        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> condition,
            IEnumerable<Expression<Func<TEntity, dynamic>>> includes,
            int take, int skip);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> condition);

        Task<int> AnyAsync(Expression<Func<TEntity, bool>> condition);

        Task DeleteAsync(Expression<Func<TEntity, bool>> condition);

        Task SaveAsync();
    }
}
