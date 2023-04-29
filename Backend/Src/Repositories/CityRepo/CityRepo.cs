namespace Backend.Src.Repositories;

using Backend.Src.Db;
using Backend.Src.Models;

public class CityRepo : BaseRepoName<City>
{
    public CityRepo(AppDbContext context) : base(context)
    {
    }
}
