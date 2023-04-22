namespace Backend.Src.Repositories.WantedRepo;

using Backend.Src.Repositories.BaseRepo;
using Backend.Src.Db;
using Backend.Src.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Src.DTOs.Filter;

public class WantedRepo : BaseRepo<Wanted>, IWantedRepo
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
            query = query.Where(wanted => wanted.User.Location.City == wantedFilter.City);
            if (wantedFilter.Instrument != null)
            {
                query = query.Where(wanted => wanted.Instrument.Name.Contains(wantedFilter.Instrument));
            }
            if (wantedFilter.Genre != null)
            {
                query = query.Where(wanted => wanted.Genres.Select(genre => genre.Name).Any(gname => gname.Contains(wantedFilter.Genre)));
            }
            return await query.ToListAsync();
        }
        return await base.GetAllAsync(new BaseQueryOptions());
    }
}