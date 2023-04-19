namespace Backend.src.Services.GenresService;

using Backend.src.Converter.Genre;
using Backend.src.DTOs.Genre;
using Backend.src.Models;
using Backend.src.Repositories.GenreRepo;
using Backend.Src.Services.Implementation;

public class GenreService : BaseService<Genre, GenreReadDTO, GenreCreateDTO, GenreUpdateDTO>, IGenreService
{
    private readonly IGenreRepo _repo;

    public GenreService(IGenreRepo repo, IGenreConverter converter) : base(repo, converter)
    {     
        _repo = repo;   
    }
}