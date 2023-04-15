namespace Backend.src.Repositories.WantedRepo;

using Backend.src.Repositories.BaseRepo;
using Backend.Src.Db;
using Backend.Src.Models;

public class WantedRepo : BaseRepo<Wanted>, IWantedRepo
{
    public WantedRepo(AppDbContext context) : base(context)
    {}
}