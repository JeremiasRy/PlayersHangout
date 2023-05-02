namespace Backend.Src.Services;

using Backend.Src.Converters;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

public class GenreService : BaseServiceName<Genre, GenreDTO, GenreDTO, GenreDTO>
{
    public GenreService(BaseRepoName<Genre> repo, IGenreConverter converter) : base(repo, converter)
    {
    }
}