using Backend.Src.Db;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Src.Repositories;

public abstract class BaseRepoName<T> : BaseRepo<T>, IBaseRepoName<T>
    where T : HasName, new()
{
    protected BaseRepoName(AppDbContext context) : base(context)
    {
    }
    
    public async override Task<IEnumerable<T>> GetAllAsync(IFilterOptions? request)
    {
        if (request is NameFilter nameFilter && nameFilter.Name is not null)
        {
            nameFilter.Name = nameFilter.Name.ToUpperInvariant();
            return await _context
                .Set<T>()
                .AsNoTracking()
                .Where(item => item.NameNormalized.Contains(nameFilter.Name))
                .Skip(nameFilter.Skip)
                .Take(nameFilter.Limit)
                .ToListAsync();
        }
        return await base.GetAllAsync(request);
    }
    public override async Task<T> CreateOneAsync(T create)
    {
        create.NameNormalized = create.Name.ToUpperInvariant();
        _context.Add(create);
        await _context.SaveChangesAsync();
        return create;
    }
    public override async Task<T> UpdateOneAsync(T update)
    {
        update.NameNormalized = update.Name.ToUpperInvariant();
        _context.Update(update);
        await _context.SaveChangesAsync();
        return update;
    }
}
