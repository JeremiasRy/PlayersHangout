namespace Backend.Src.Controllers;

using Backend.Src.DTOs.Genre;
using Backend.Src.Models;
using Backend.Src.Services.GenresService;

public class GenreController : BaseController<Genre, GenreReadDTO, GenreCreateDTO, GenreUpdateDTO>
{
    private readonly IGenreService _service;

    public GenreController(IGenreService service) : base(service)
    {
        _service = service;
    }
    
}