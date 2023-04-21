namespace Backend.Src.Controllers;

using Backend.Src.DTOs.Genre;
using Backend.Src.Models;
using Backend.Src.Services.GenreService;

public class GenreController : BaseController<Genre, GenreDTO, GenreDTO, GenreDTO>
{
    public GenreController(IGenreService service) : base(service)
    {
    }
}