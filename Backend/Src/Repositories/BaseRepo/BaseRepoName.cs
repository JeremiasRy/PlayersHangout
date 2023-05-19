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
        if (request is NameFilter nameFilter)
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .Where(item => item.Name.Contains(nameFilter.Name))
                .Skip(nameFilter.Skip)
                .Take(nameFilter.Limit)
                .ToListAsync();
        }
        return await base.GetAllAsync(request);
    }
}
