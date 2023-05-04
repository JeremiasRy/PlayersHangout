namespace Backend.Src.Repositories;

using Backend.Src.Db;
using Backend.Src.Models;

public class LocationRepo : BaseRepo<Location>, ILocationRepo
{
    public LocationRepo(AppDbContext context) : base(context)
    {
    }
}
