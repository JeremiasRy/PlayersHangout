using Backend.Src.Db;
using Backend.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Src.Repositories;

public abstract class BaseRepo<T> : IBaseRepo<T>
    where T : BaseModel, new()
{
    protected readonly AppDbContext _context;

    public BaseRepo(AppDbContext context)
    {
        _context = context;
    }

    public virtual async Task<T> CreateOneAsync(T create)
    {
        _context.Add(create);
        await _context.SaveChangesAsync();
        return create;
    }

    public async Task DeleteOneAsync(T item)
    {
        _context.Remove(item);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(IFilterOptions? request)
    {
        var query = _context.Set<T>().AsNoTracking().Where(c => true);

        if (request is BaseQueryOptions filter)
        {
            return await query
                .Skip(filter.Skip)
                .Take(filter.Limit)
                .ToListAsync();
        }
        return await query
            .Skip(0)
            .Take(30)
            .ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context
            .Set<T>()
            .AsNoTracking()
            .SingleOrDefaultAsync(model => model.Id == id);
    }

    public virtual async Task<T> UpdateOneAsync(T update)
    {
        _context.Update(update);
        await _context.SaveChangesAsync();
        return update;
    }

}