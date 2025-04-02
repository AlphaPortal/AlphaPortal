using Data.Contexts;
using Data.Interfaces;
using Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _db = context.Set<TEntity>();

    public virtual async Task<RepositoryResult> AddAsync(TEntity entity)
    {
        try
        {
            if (entity == null)
            {
                return new RepositoryResult { Succeeded = false, StatusCode = 400, Error = "Invalid properties." };
            }
            await _db.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult { Succeeded = true, StatusCode = 202 };

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
        }
    }

    public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync(bool orderByDescending = false, Expression<Func<TEntity, object>>? sortByColumn = null, Expression<Func<TEntity, bool>>? filterBy = null, int take = 0, Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _db;

        if (filterBy != null)
        {
            query = query.Where(filterBy);
        }

        if (includes != null && includes.Length != 0)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }

        if (sortByColumn != null)
        {
            query = orderByDescending ? query.OrderByDescending(sortByColumn) : query.OrderBy(sortByColumn);
        }

        if (take > 0)
        {
            query.Take(take);
        }

        var entities = await query.ToListAsync();
        return new RepositoryResult<IEnumerable<TEntity>> { Succeeded = true, StatusCode = 200, Result = entities };

    }

    public virtual async Task<RepositoryResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> findBy, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _db;

        if (findBy == null)
        {
            return new RepositoryResult<TEntity> { Succeeded = false, StatusCode = 400, Error = "Expression not defined." };
        }

        if (includes != null && includes.Length != 0)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }

        var entity = await query.FirstOrDefaultAsync(findBy);
        return entity != default
            ? new RepositoryResult<TEntity> { Succeeded = true, StatusCode = 200, Result = entity }
            : new RepositoryResult<TEntity> { Succeeded = false, StatusCode = 404, Error = "Entity not found." };
    }

    public virtual async Task<RepositoryResult> UpdateAsync(TEntity entity)
    {
        try
        {
            if (entity == null)
            {
                return new RepositoryResult { Succeeded = false, StatusCode = 400, Error = "Invalid properties." };
            }

            var result = await _db.ContainsAsync(entity);
            if (!result)
            {
                return new RepositoryResult { Succeeded = false, StatusCode = 404, Error = "Entity no found" };
            }

            _db.Update(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult { Succeeded = true, StatusCode = 200 };

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
        }
    }

    public virtual async Task<RepositoryResult> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
        {
            return new RepositoryResult { Succeeded = false, StatusCode = 400, Error = "Invalid expression." };
        }

        var result = await _db.AnyAsync(expression);
        if (!result)
        {
            return new RepositoryResult { Succeeded = false, StatusCode = 404, Error = "Entity not found." };
        }

        return new RepositoryResult { Succeeded = true, StatusCode = 200, Error = "Entity exists" };
    }

    public virtual async Task<RepositoryResult> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            if (expression == null)
            {
                return new RepositoryResult { Succeeded = false, StatusCode = 400, Error = "Invalid properties." };
            }

            var entity = await _db.FirstOrDefaultAsync(expression);
            if (entity == null)
            {
                return new RepositoryResult { Succeeded = false, StatusCode = 404, Error = "Entity no found" };
            }

            _db.Remove(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult { Succeeded = true, StatusCode = 200 };

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
        }
    }
}
