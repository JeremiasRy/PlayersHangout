namespace Backend.Src.Repositories;

using Backend.Src.Db;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Microsoft.EntityFrameworkCore;

public class CityRepo : BaseRepo<City>
{
    public CityRepo(AppDbContext context) : base(context)
    {
    }
    public override async Task<IEnumerable<City>> GetAllAsync(IFilterOptions? request)
    {
        var query = _context.Cities.Where(c => true);
        if (request is NameFilter nameFilter)
        {
            return await query
                .Where(city => city.Name == nameFilter.Name)
                .Skip(nameFilter.Skip)
                .Take(nameFilter.Limit)
                .ToListAsync();
        }
        if (request is BaseQueryOptions baseQueryOptions)
        {
            return await query
                .Skip(baseQueryOptions.Skip)
                .Take(baseQueryOptions.Limit)
                .ToListAsync();
        }
        return await query
                .Skip(0)
                .Take(30)
                .ToListAsync();
    }
}
