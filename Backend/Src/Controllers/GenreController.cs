namespace Backend.src.Controllers;

using Backend.src.DTOs.Genre;
using Backend.src.Models;
using Backend.src.Services.GenresService;

public class GenreController : BaseController<Genre, GenreReadDTO, GenreCreateDTO, GenreUpdateDTO>
{
    private readonly IGenreService _service;

    public GenreController(IGenreService service) : base(service)
    {
        _service = service;
    }
    
}