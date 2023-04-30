using Backend.Src.Db;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Castle.DynamicProxy.Contributors;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Backend.Src.Repositories;

public abstract class BaseRepoName<T> : BaseRepo<T>
    where T : HasName, new()
{
    protected BaseRepoName(AppDbContext context) : base(context)
    {
    }
    public async override Task<T?> CreateOneAsync(T create)
    {
        var check = await _context
            .Set<T>()
            .AsNoTracking()
            .SingleOrDefaultAsync(item => item.Name == create.Name);
        if (check != null)
        {
            return null;
        }
        return await base.CreateOneAsync(create);
    }
    public async override Task<IEnumerable<T>> GetAllAsync(IFilterOptions? request)
    {
        if (request is NameFilter nameFilter)
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .Where(item => item.Name == nameFilter.Name)
                .Skip(nameFilter.Skip)
                .Take(nameFilter.Limit)
                .ToListAsync();
        }
        return await base.GetAllAsync(request);
    }
}
