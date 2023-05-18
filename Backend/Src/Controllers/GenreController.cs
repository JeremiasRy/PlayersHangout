namespace Backend.Src.Controllers;

using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Services;

public class GenreController : BaseController<Genre, GenreDTO, GenreDTO, GenreDTO>
{
    public GenreController(IGenreService service) : base(service)
    {
    }
}