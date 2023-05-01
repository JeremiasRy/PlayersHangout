namespace Backend.Src.Repositories;

using Backend.Src.Db;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class WantedRepo : BaseRepo<Wanted>
{
    public WantedRepo(AppDbContext context) : base(context)
    {
    }
    public override async Task<IEnumerable<Wanted>> GetAllAsync(IFilterOptions? request)
    {
        var query = _context.Wanteds.Where(wanted => !wanted.Fullfilled).AsNoTracking();

        if (request is BaseQueryOptions filter)
        {
            return await base.GetAllAsync(filter);
        }
        if (request is MatchDTO wantedFilter)
        {
            query = query.Where(wanted => wanted.User.Location.City.Name == wantedFilter.City);
            if (wantedFilter.Instrument != null)
            {
                query = query.Where(wanted => wanted.Instrument.Name.Contains(wantedFilter.Instrument));
            }
            if (wantedFilter.Genre != null)
            {
                query = query.Where(wanted => wanted.Genres.Select(genre => genre.Name).Any(gname => gname.Contains(wantedFilter.Genre)));
            }
            return await query
                .Skip(wantedFilter.Skip)
                .Take(wantedFilter.Limit)
                .ToListAsync();
        }
        return await base.GetAllAsync(new BaseQueryOptions());
    }
}