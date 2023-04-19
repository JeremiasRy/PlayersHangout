namespace Backend.Src.Services.GenresService;

using Backend.src.Converter.Genre;
using Backend.Src.DTOs.Genre;
using Backend.Src.Models;
using Backend.Src.Repositories.GenreRepo;
using Backend.Src.Services.Implementation;

public class GenreService : BaseService<Genre, GenreReadDTO, GenreCreateDTO, GenreUpdateDTO>, IGenreService
{
    private readonly IGenreRepo _repo;

    public GenreService(IGenreRepo repo, IGenreConverter converter) : base(repo, converter)
    {     
        _repo = repo;   
    }
}