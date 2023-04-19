namespace Backend.Src.Repositories.GenreRepo;

using Backend.Src.Models;
using Backend.Src.Repositories.BaseRepo;
using Backend.Src.Db;

public class GenreRepo : BaseRepo<Genre>, IGenreRepo
{
    public GenreRepo(AppDbContext context) : base(context)
    {}
}