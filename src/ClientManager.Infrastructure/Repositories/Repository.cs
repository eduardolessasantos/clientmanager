using System.Linq.Expressions;
using ClientManager.Core.Interfaces;
using ClientManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClientManager.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ClientManagerDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(ClientManagerDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<int> ProcessBatchAsync(IEnumerable<T> toAdd, IEnumerable<T> toUpdate, IEnumerable<T> toRemove)
    {
        if (toAdd != null && toAdd.Any())
            await _dbSet.AddRangeAsync(toAdd);

        if (toUpdate != null && toUpdate.Any())
            _dbSet.UpdateRange(toUpdate);

        if (toRemove != null && toRemove.Any())
            _dbSet.RemoveRange(toRemove);

        return await _context.SaveChangesAsync();
    }
}
