using Backend.Src.Db;
using Backend.Src.Models;
using Backend.Src.Repositories.BaseRepo;

namespace Backend.Src.Repositories.LocationRepo;

public class LocationRepo : BaseRepo<Location>
{
    public LocationRepo(AppDbContext context) : base(context)
    {
    }
}
