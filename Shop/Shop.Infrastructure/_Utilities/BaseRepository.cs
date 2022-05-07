using Common.Domain.Bases;
using Common.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure._Utilities;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ShopContext _context;
    public BaseRepository(ShopContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity?> GetAsync(long id)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(t => t.Id.Equals(id)); ;
    }
    public async Task<TEntity?> GetTracking(long id)
    {
        return await _context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));

    }
    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    void IBaseRepository<TEntity>.Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public async Task AddRange(ICollection<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
    }
    public void Update(TEntity entity)
    {
        _context.Update(entity);
    }
    public async Task<int> Save()
    {
        return await _context.SaveChangesAsync();
    }
    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().AnyAsync(expression);
    }
    public bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().Any(expression);
    }

    public TEntity? Get(long id)
    {
        return _context.Set<TEntity>().FirstOrDefault(t => t.Id.Equals(id)); 
    }
}
