using Backend.Src.Converter;
using Backend.Src.Db;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Src.Services;

public class BaseServiceNameFilter<T, TDto> : BaseService<T, TDto>
    where T : WithName, new()
    where TDto : new()
{
    public BaseServiceNameFilter(IConverter converter, AppDbContext appDbContext) : base(converter, appDbContext)
    {
    }
    public override async Task<ICollection<T>> GetAllAsync(IFilterOptions? request)
    {
        if (request is NameFilter nameFilter)
        {
            return await _appDbContext
                .Set<T>()
                .Where(item => item.Name.Contains(nameFilter.Name))
                .Skip(nameFilter.Skip)
                .Take(nameFilter.Limit)
                .ToListAsync();
        }
        return await base.GetAllAsync(new BaseQueryOptions());
    }
}
