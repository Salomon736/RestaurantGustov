using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.DataBase.EntityFramework.Entities;
using Restaurant.Infrastructure.DataBase.EntityFramework.Context;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Repositories.Common;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity , IIdentifiable
{
    private readonly MenuDBcontext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(MenuDBcontext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var entityEntry = await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entityEntry = _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var del = await _dbSet.FindAsync(id);
        if (del is null) return false;
        
        _dbSet.Remove(del);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<bool> IsExistId(int id)
    {
        return await _dbSet.AnyAsync(x=>x.Id == id);
    }

    // public async Task<bool> DeleteHardAsync(int id)
    // {
    //     var entity = await _dbSet.FindAsync(id);
    //     if (entity is null) return false;
    //
    //     _dbSet.Remove(entity);
    //     return true;
    // }
}