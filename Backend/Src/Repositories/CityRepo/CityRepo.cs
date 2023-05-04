namespace Backend.Src.Repositories;

using Backend.Src.Db;
using Backend.Src.Models;

public class CityRepo : BaseRepo<City>, ICityRepo
{
    public CityRepo(AppDbContext context) : base(context)
    {
    }
}
