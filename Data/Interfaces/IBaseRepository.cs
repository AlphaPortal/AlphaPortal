using Domain.Responses;
using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<RepositoryResult> AddAsync(TEntity entity);
        Task<RepositoryResult> DeleteAsync(Expression<Func<TEntity, bool>> expression);
        Task<RepositoryResult> ExistsAsync(Expression<Func<TEntity, bool>> expression);
        Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync(bool orderByDescending = false, Expression<Func<TEntity, object>>? sortByColumn = null, Expression<Func<TEntity, bool>>? filterBy = null, int take = 0, Expression<Func<TEntity, object>>[] includes = null);
        Task<RepositoryResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> findBy, params Expression<Func<TEntity, object>>[] includes);
        Task<RepositoryResult> UpdateAsync(TEntity entity);
    }
}