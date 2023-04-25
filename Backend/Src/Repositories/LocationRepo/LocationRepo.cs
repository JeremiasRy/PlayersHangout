namespace Backend.Src.Repositories;

using Backend.Src.Db;
using Backend.Src.Models;

public class LocationRepo : BaseRepo<Location>
{
    public LocationRepo(AppDbContext context) : base(context)
    {
    }
}
