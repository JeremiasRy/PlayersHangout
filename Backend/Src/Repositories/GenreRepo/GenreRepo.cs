namespace Backend.Src.Repositories;

using Backend.Src.Db;
using Backend.Src.Models;

public class GenreRepo : BaseRepoName<Genre>
{
    public GenreRepo(AppDbContext context) : base(context)
    { }
}