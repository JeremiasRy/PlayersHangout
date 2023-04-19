namespace Backend.src.Repositories.GenreRepo;

using Backend.src.Models;
using Backend.src.Repositories.BaseRepo;
using Backend.Src.Db;

public class GenreRepo : BaseRepo<Genre>, IGenreRepo
{
    public GenreRepo(AppDbContext context) : base(context)
    {}
}