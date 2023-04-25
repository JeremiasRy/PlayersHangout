namespace Backend.Src.Repositories;

using Backend.Src.Models;
using Backend.Src.Db;

public class GenreRepo : BaseRepo<Genre>
{
    public GenreRepo(AppDbContext context) : base(context)
    {}
}