namespace Backend.Src.Repositories;

using Backend.Src.Db;
using Backend.Src.Models;

public class CityRepo : BaseRepoName<City>, ICityRepo
{
    public CityRepo(AppDbContext context) : base(context)
    {
    }
}
