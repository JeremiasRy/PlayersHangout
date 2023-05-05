using Backend.Src.Db;
using Backend.Src.Models;

namespace Backend.Src.Repositories;

public class GenreRepo : BaseRepoName<Genre>, IGenreRepo
{
    public GenreRepo(AppDbContext context) : base(context)
    { }
}