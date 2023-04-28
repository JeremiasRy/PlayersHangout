namespace Backend.Src.Repositories;

using Backend.Src.Models;
using Backend.Src.Db;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.Src.DTOs;
using Microsoft.EntityFrameworkCore;

public class GenreRepo : BaseRepo<Genre>
{
    public GenreRepo(AppDbContext context) : base(context)
    {}
    public override async Task<IEnumerable<Genre>> GetAllAsync(IFilterOptions? request)
    {
        var query = _context.Genres.Where(c => true);
        if (request is NameFilter nameFilter)
        {
            query = query.Where(genre => genre.Name == nameFilter.Name);
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