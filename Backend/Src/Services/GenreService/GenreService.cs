namespace Backend.Src.Services;

using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;

namespace Backend.Src.Services;

public class GenreService : BaseServiceName<Genre, GenreDTO, GenreDTO, GenreDTO>, IGenreService
{
    public GenreService(BaseRepoName<Genre> repo, IConverter converter) : base(repo, converter)
    {
    }
}