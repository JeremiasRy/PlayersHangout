namespace Backend.Src.Services.GenreService;

using Backend.Src.Converter.Genre;
using Backend.Src.DTOs.Genre;
using Backend.Src.Models;
using Backend.Src.Repositories.GenreRepo;
using Backend.Src.Services.Implementation;

public class GenreService : BaseService<Genre, GenreDTO, GenreDTO, GenreDTO>, IGenreService
{
    public GenreService(IGenreRepo repo, IGenreConverter converter) : base(repo, converter)
    {      
    }
}